namespace backend.Repositories;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;

public class ForumRepository : IRepository<Forum> {
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
}