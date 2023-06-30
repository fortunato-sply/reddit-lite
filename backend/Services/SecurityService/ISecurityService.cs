namespace backend.Services;

public interface ISecurityService
{
  string ApplyHash(string password, string salt);
  string GenerateSalt(int len);
}