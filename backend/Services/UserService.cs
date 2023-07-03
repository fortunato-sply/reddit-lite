using Security.Jwt;

using backend.Model;
using backend.Repositories;

public interface IUserService
{
  Task<DataUser> ValidateUserToken(Jwt jwt);
}

public class UserService : IUserService
{
  private IJwtService jwtService;
  private IUserRepository userRepository;

  public UserService(IJwtService jwtService, IUserRepository userRepository)
  {
    this.jwtService = jwtService;
    this.userRepository = userRepository;
  }

  public async Task<DataUser> ValidateUserToken(Jwt jwt)
  {
    DataUser user;

    var token = jwtService.Validate<UserToken>(jwt.Value);

    if(!token.Authenticated)
      throw new InvalidDataException();

    user = await userRepository.GetUserByUsername(token.Username);
    return user;
  }
}

