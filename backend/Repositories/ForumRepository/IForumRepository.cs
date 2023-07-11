namespace backend.Repositories;

using Model;

public interface IForumRepository : IRepository<Forum> 
{
  Task<List<ForumDTO>> GetUserForums(int userId);
  Task<List<ForumDTO>> GetUserFavoriteForums(int userId);
  Task<int> GetLastForumID(); 
  Task<List<ForumDTO>> GetForums();
  Task<ForumDTO> GetForumDTOByID(int id);
  Task<Forum> GetForumByID(int id);
  Task<bool> IsUserFollowingForum(int userId, int forumId);
  Task StartFollowingForum(int userId, int forumId);
  Task StopFollowingForum(int userId, int forumId);

  Task<bool> IsForumFavorite(int userId, int forumId);
  Task FavoriteForum(int userId, int forumId);
  Task UnfavoriteForum(int userId, int forumId);
  Task<List<UserMemberDTO>> GetMembers(int forumId);
  Task AddRelationship(int forumId, int userId);
}
