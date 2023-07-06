namespace backend.Repositories;

using Model;

public interface IForumRepository : IRepository<Forum> 
{
  Task<List<ForumDTO>> GetUserForums(int userId);
  Task<List<ForumDTO>> GetUserFavoriteForums(int userId);
  Task<int> GetLastForumID(); 
  Task<ForumDTO> GetForumDTOByID(int id);
  Task<Forum> GetForumByID(int id);

  Task<bool> IsUserFollowingForum(int userId, int forumId);
  Task StartFollowingForum(int userId, int forumId);
  Task StopFollowingForum(int userId, int forumId);
}
