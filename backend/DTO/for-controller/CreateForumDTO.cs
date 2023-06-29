public class CreateForumDTO
{
  public string JwtToken { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public IFormFile PhotoFile { get; set; }
  public string PhotoName { get; set; }
}