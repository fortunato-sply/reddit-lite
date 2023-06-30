namespace backend.Services;

public interface ISecurityService
{
  string GenerateSalt();
  string ApplyHash(string password, string salt);
}