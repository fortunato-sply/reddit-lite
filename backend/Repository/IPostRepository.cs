namespace backend.Repositories;

using Model;

public interface IPostRepository : IRepository<Post> 
{
  IEnumerable<PostDTO> GetCompletePosts();
  IEnumerable<PostDTO> GetOrderedPosts();
  PostDTO? GetPostById(int id);
}