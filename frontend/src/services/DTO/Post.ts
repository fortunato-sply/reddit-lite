export interface CompletePost {
  idAuthor: number
  photo: string
  authorName: string
  content: string
  createdAt: Date
  likes: number,
  forumName: string
}

export interface PostDTO {
  content: string,
  userId: number,
  forumId: string
}

// public class CompletePost {
//   public string? AuthorName { get; set; }
//   public int IdAuthor { get; set; }
//   public string ? Content { get; set; }
//   public DateTime ? CreatedAt { get; set; }
//   public int ? Likes { get; set; }
//   public ICollection<Comment> ? Comments { get; set; }
//   public int IdPost { get; set; }
//   public int ? IdForum { get; set; }
//   public string ? ForumName { get; set; }
// }