public class UserToken
{
  public bool Authenticated { get; set; }
  public int Id { get; set; }
  public string? Username { get; set; }
  public string? Email { get; set; }
  public DateTime? Born { get; set; }
  public int? PhotoID { get; set; }
}