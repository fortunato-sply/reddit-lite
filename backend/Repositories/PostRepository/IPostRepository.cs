namespace backend.Repositories;

using Model;

public interface IPostRepository : IRepository<Post> 
{
  Task<List<PostDTO>> GetOrderedPosts();
  Task<List<PostDTO>> GetForumPosts(int forumId);
}