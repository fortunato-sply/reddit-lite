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
        Photo = i.Photo,
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
        Photo = i.Photo,
        CreatedAt = f.CreatedAt,
        Owner = u.Username
      };
      
    return await response.ToListAsync();
  }

}
