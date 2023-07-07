using backend.Model;

public class PostDTO
{
  public string AuthorName { get; set; }
  public string ForumName { get; set; }
  public int IdAuthor { get; set; }
  public int? Photo { get; set; }
  public string Content { get; set; }
  public DateTime? CreatedAt { get; set; }
  public int? Likes { get; set; }
}