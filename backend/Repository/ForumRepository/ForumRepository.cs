namespace backend.Repositories;

using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

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

  public Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
  {
    var result = context.Forums.Where(exp);
    return result.ToListAsync();
  }

  public Task<List<ForumDTO>> GetUserForums(int userId)
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
        Title = f.Title,
        Description = f.Description,
        Photo = i.Photo,
        CreatedAt = f.CreatedAt,
        Owner = u.Username
      };
      
  return response.ToListAsync();
  }

  public IEnumerable<Forum> GetUserFavoriteForums(DataUser user)
  {

  }
}