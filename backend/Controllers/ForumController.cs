using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model;
using backend.Repositories;

namespace backend.Controllers;

[EnableCors("MainPolicy")]
[ApiController]
[Route("forum")]
public class ForumController : ControllerBase
{
  [HttpGet("search/{name}")]
  public async Task<ActionResult<Forum>> SearchForumByName(
    string name,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.Filter(f => f.Title.Contains(name));

    if (forums.Count < 1)
      return BadRequest();
    
    return Ok(forums.First());
  }

  [HttpGet("myforums")]
  public async Task<ActionResult<List<ForumDTO>>> GetUserForums(
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    var jwt = Request.Headers["jwt"];
    Console.WriteLine(jwt);
    UserToken user = await userService.ValidateUserToken(new Jwt { Value = jwt });
    var forums = await repo.GetUserForums(user.Id);
    if(forums.Count < 1)
      return Ok(new List<ForumDTO>(0));

    return Ok(forums);
  }

  [HttpGet("favorites/{userId}")]
  public async Task<ActionResult<List<ForumDTO>>> GetUserFavoriteForums(
    int userId,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.GetUserFavoriteForums(userId);

    if(forums.Count < 1)
      return StatusCode(404);
      
    return Ok(forums);
  }

  [HttpPost("create")]
  public async Task<ActionResult<int>> Create(
    [FromBody] CreateForumDTO data,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    var query = await repo.Filter(f => f.Title == data.Title);
    if (query.Count > 0)
      return BadRequest("Fórum já existente. Utilize outro nome.");

    UserToken user = await userService.ValidateUserToken(new Jwt { Value = data.JwtToken });

    Forum forum = new Forum
    {
      Title = data.Title,
      Description = data.Description,
      CreatedAt = DateTime.Now,
      Owner = user.Id
    };
    
    await repo.Add(forum);

    var id = await repo.GetLastForumID();
    return Ok(id);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ForumDTO>> GetForumByID(
    int id,
    [FromServices] IForumRepository repo
  )
  {
    var forum = await repo.GetForumDTOByID(id);

    if (forum == null)
      return BadRequest("fórum não encontrado");
    
    return Ok(forum);
  }
}