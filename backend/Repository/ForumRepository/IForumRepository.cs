namespace backend.Repositories;

using Model;

public interface IForumRepository : IRepository<Forum> 
{
  Task<List<ForumDTO>> GetUserForums(int userId);
  IEnumerable<Forum> GetUserFavoriteForums(int userId);

}
