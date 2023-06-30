using System.Security.Cryptography;
using System.Text;

namespace backend.Services;

public class SecurityService : ISecurityService
{
  public string ApplyHash(string password, string salt)
  {
    string passwordSalt = password + salt;

    using var sha = SHA256.Create();
    var bytes = Encoding.UTF8.GetBytes(passwordSalt);
    var hashBytes = sha.ComputeHash(bytes);
    var hash = Convert.ToBase64String(hashBytes);

    return hash;
  }

  public string GenerateSalt(int len)
  {
    byte[] bytes = new byte[len];
    Random.Shared.NextBytes(bytes);
    string str = Convert.ToBase64String(bytes);
    
    return str;
  }
}