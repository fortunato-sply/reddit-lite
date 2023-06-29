namespace backend.Repositories;

using Model;

public interface IPostRepository : IRepository<Post> 
{
  Task<IEnumerable<PostDTO>> GetOrderedPosts();
  Task<PostDTO?> GetPostById(int id);
}