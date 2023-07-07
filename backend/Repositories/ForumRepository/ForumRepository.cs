namespace backend.Repositories;

using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

using System.IO;

public class ForumRepository : IForumRepository {
  private RedditliteContext context;
  public ForumRepository(RedditliteContext context)
    => this.context = context;

  public async Task Save()
   => await this.context.SaveChangesAsync();

  public async Task Add(Forum forum)
  {
    await context.Forums.AddAsync(forum);
    await context.SaveChangesAsync();
  }

  public async Task AddRelationship(int forumId, int userId)
  {
    ForumXuser fxu = new ForumXuser
    {
      FkForum = forumId,
      FkUser = userId
    };

    await context.ForumXusers.AddAsync(fxu);
    await context.SaveChangesAsync();
  }

  public async Task Delete(Forum forum)
  {
    context.Forums.Remove(forum);
    await context.SaveChangesAsync();
  }

  public async Task Update(Forum forum)
  {
    context.Forums.Update(forum);
    await context.SaveChangesAsync();
  }

  public async Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
  {
    var result = context.Forums.Where(exp);
    return await result.ToListAsync();
  }

  public async Task<List<ForumDTO>> GetUserForums(int userId)
  {
    var response = 
      from f in context.Forums
      join u in context.DataUsers
        on f.Owner equals u.Id
      join i in context.ImageData
        on f.Photo equals i.Id
      where f.Owner == userId
      select new ForumDTO
      {
        Id = f.Id,
        Title = f.Title,
        Description = f.Description,
        Photo = i.Id,
        CreatedAt = f.CreatedAt,
        Owner = u.Username
      };
      
    return await response.ToListAsync();
  }

  public async Task<List<ForumDTO>> GetUserFavoriteForums(int userId)
  {
    var response =
      from fxu in context.Favorites
      join u in context.DataUsers
        on fxu.FkUser equals u.Id
      join f in context.Forums
        on fxu.FkForum equals f.Id
      join i in context.ImageData
        on f.Photo equals i.Id
      where fxu.FkUser == userId
      select new ForumDTO
      {
        Id = f.Id,
        Title = f.Title,
        Description = f.Description,
        Photo = i.Id,
        CreatedAt = f.CreatedAt,
        Owner = u.Username
      };
      
    return await response.ToListAsync();
  }

  public async Task<int> GetLastForumID()
  {
    var forum = await context.Forums.OrderByDescending(f => f.CreatedAt).FirstOrDefaultAsync();
    return forum.Id;
  }

  public async Task<ForumDTO> GetForumDTOByID(int id)
  {
    var response = 
      from f in context.Forums
      join u in context.DataUsers
        on f.Owner equals u.Id
      where f.Id == id
      select new ForumDTO
      {
        Id = f.Id,
        Title = f.Title,
        Description = f.Description,
        Photo = f.Photo,
        CreatedAt = f.CreatedAt,
        Owner = u.Username
      };
    
    return await response.FirstOrDefaultAsync();
  }

  public async Task<Forum> GetForumByID(int id)
  {
    var query = context.Forums.Where(f => f.Id == id);
    return await query.FirstOrDefaultAsync();
  }

  public async Task<bool> IsUserFollowingForum(int userId, int forumId)
  {
    var query = await context.ForumXusers.Where(f => f.FkUser == userId & f.FkForum == forumId).ToListAsync();
    Console.WriteLine("count query: " + query.Count);
    if(query.Count > 0)
      return true;
    
    return false;
  }

  public async Task StartFollowingForum(int userId, int forumId)
  {
    ForumXuser fxu = new ForumXuser
    {
      FkForum = forumId,
      FkUser = userId
    };

    await context.ForumXusers.AddAsync(fxu);
    await context.SaveChangesAsync();
  }

  public async Task StopFollowingForum(int userId, int forumId)
  {
    var query = await context.ForumXusers.Where(f => f.FkForum == forumId && f.FkUser == userId).FirstOrDefaultAsync();

    context.ForumXusers.Remove(query);
    await context.SaveChangesAsync();
  }

  public async Task<bool> IsForumFavorite(int userId, int forumId)
  {
    var query = await context.Favorites.Where(f => f.FkForum == forumId && f.FkUser == userId).ToListAsync();
    if(query.Count > 0)
      return true;
    
    return false;
  }

  public async Task FavoriteForum(int userId, int forumId)
  {
    Favorite fav = new Favorite
    {
      FkForum = forumId,
      FkUser = userId
    };

    await context.Favorites.AddAsync(fav);
    await context.SaveChangesAsync();
  }

  public async Task UnfavoriteForum(int userId, int forumId)
  {
    var query = await context.Favorites.Where(f => f.FkForum == forumId && f.FkUser == userId).FirstOrDefaultAsync();

    context.Favorites.Remove(query);
    await context.SaveChangesAsync();
  }

  public async Task<List<UserMemberDTO>> GetMembers(int forumId)
  {
    var query = 
      from fxu in context.ForumXusers
      join f in context.Forums
        on fxu.FkForum equals f.Id
      join u in context.DataUsers
        on fxu.FkUser equals u.Id
      where f.Id == forumId
      select new UserMemberDTO
      {
        Username = u.Username,
        Photo = u.Photo
      };
    
    return await query.ToListAsync();
  }
}
