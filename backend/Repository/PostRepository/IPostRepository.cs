namespace backend.Repositories;

using Model;

public interface IPostRepository : IRepository<Post> 
{
  IEnumerable<PostDTO> GetCompletePosts();
  Task<IEnumerable<PostDTO>> GetOrderedPosts();
  Task<PostDTO?> GetPostById(int id);
}