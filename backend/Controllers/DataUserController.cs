using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model;
using backend.Repositories;
using backend.Services;
using Securitas.JWT;

namespace backend.Controllers;

[EnableCors("MainPolicy")]
[ApiController]
[Route("user")]
public class DataUserController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<DataUser>> GetUserById(
      int id,
      [FromServices] IRepository<DataUser> repo
      )
    {
      var user = await repo.Filter(u => u.Id == id);
      
      if (user.Count < 1)
        return BadRequest();

      return Ok(user.First());
    }

    [HttpPost("signup")]
    public async Task<ActionResult<UserToken>> SignUp(
      [FromBody] SignUpDTO userData,
      [FromServices] IUserRepository repo,
      [FromServices] IImageService imageService,
      [FromServices] ISecurityService security,
      [FromServices] IJWTService jwt
    )
    {
      var query = await repo.Filter(user => user.Email == userData.Email || user.Username == userData.Username);
      bool emailOrUsernameExists = query.Count != 0;

      if(emailOrUsernameExists)
        return BadRequest("E-mail ou usuário já existem.");

      var salt = security.GenerateSalt(8);

      DataUser newUser = new DataUser
      {
        Email = userData.Email,
        Username = userData.Username,
        Password = security.ApplyHash(userData.Password, salt),
        Salt = salt,
        Born = userData.Born ?? new DateTime()
      };

      await repo.Add(newUser);
      var createdUser = await repo.GetUserByUsername(userData.Username);

      UserToken tokendata = new UserToken
      {
        Id = createdUser.Id,
        Username = createdUser.Username,
        Email = createdUser.Email,
        Born = createdUser.Born,
        PhotoID = createdUser.Photo,
        Authenticated = true
      };

      var token = jwt.GenerateToken(tokendata);
      return Ok(new Jwt{ Value = token });
    }
  
  [HttpPost("signin")]
  public async Task<ActionResult<UserToken>> SignIn(
    [FromBody] LoginDTO userData,
    [FromServices] IUserRepository repo,
    [FromServices] IJWTService jwt,
    [FromServices] ISecurityService security
  )
  {
    var user = await repo.GetUserByUsername(userData.Username);

    if(user is null)
      return BadRequest("Nome de usuário incorreto.");
    
    var passwordHash = security.ApplyHash(userData.Password, user.Salt);

    if(passwordHash != user.Password)
      return BadRequest("Senha incorreta.");

    var userToken = new UserToken
    {
      Id = user.Id,
      Username = user.Username,
      Email = user.Email,
      Born = user.Born,
      PhotoID = user.Photo,
      Authenticated = true
    };
    
    var token = jwt.GenerateToken(userToken);
    return Ok(new Jwt{ Value = token });
  }

  [HttpPost("validate")]
  public async Task<ActionResult<UserToken>> ValidateJwt(
    [FromServices] IJWTService jwtService,
    [FromBody] Jwt jwt
  )
  {
    if (jwt.Value == "" || jwt.Value is null)
      return Ok (new UserToken { Authenticated = false });

    try
    {
      var result = jwtService.ValidateToken<UserToken>(jwt.Value).Data;
      return Ok(result);
    }
    catch (Exception e)
    {
      return Ok(new UserToken { Authenticated = false });
    }
  }
}
