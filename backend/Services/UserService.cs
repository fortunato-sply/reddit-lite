using Securitas.JWT;

using backend.Model;
using backend.Repositories;

public interface IUserService
{
  Task<UserToken> ValidateUserToken(Jwt jwt);
}

public class UserService : IUserService
{
  private IJWTService jwtService;
  private IUserRepository userRepository;

  public UserService(IJWTService jwtService, IUserRepository userRepository)
  {
    this.jwtService = jwtService;
    this.userRepository = userRepository;
  }

  public async Task<UserToken> ValidateUserToken(Jwt jwt)
  {
    var token = jwtService.ValidateToken<UserToken>(jwt.Value).Data;

    if(!token.Authenticated)
    {
      Console.WriteLine("nao-autenticado");
      throw new InvalidDataException();
    }

    Console.WriteLine("autenticado");
    var user = await userRepository.GetUserByUsername(token.Username);

    if (user is null)
    {
      throw new InvalidDataException();
    }

    var userToken = new UserToken
    {
      Id = user.Id,
      Authenticated = true,
      Email = user.Email,
      Username = user.Username,
      Born = user.Born,
      PhotoID = user.Photo
    };

    return userToken;
  }
}

