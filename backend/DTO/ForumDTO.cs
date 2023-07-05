public class ForumDTO
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public int? Photo { get; set; }
  public DateTime? CreatedAt { get; set; }
  public string? Owner { get; set; }
}