using backend.Model;

public class PostDTO
{
  public string? AuthorName { get; set; }
  public int IdAuthor { get; set; }
  public string? Content { get; set; }
  public DateTime? CreatedAt { get; set; }
  public int? Likes { get; set; }
  public ICollection<Comment>? Comments { get; set; }
  public int IdPost { get; set; }
  public int? IdForum { get; set; }
  public string? ForumName { get; set; }
}