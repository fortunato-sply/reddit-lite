public class SignUpDTO
{
  public string Username { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public DateOnly Born { get; set; }
  public IFormFile Photo { get; set; }
  public string PhotoName { get; set; }
}