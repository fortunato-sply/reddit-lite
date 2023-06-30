namespace backend.Repositories;

using System.Linq.Expressions;
using backend.Model;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
  private RedditliteContext context;
  public UserRepository(RedditliteContext context)
    => this.context = context;

  public async Task Save()
   => await this.context.SaveChangesAsync();

  public async Task Add(DataUser user)
  {
    await context.DataUsers.AddAsync(user);
    await context.SaveChangesAsync();
  }

  public async Task Delete(DataUser user)
  {
    context.DataUsers.Remove(user);
    await context.SaveChangesAsync();
  }

  public async Task Update(DataUser user)
  {
    context.DataUsers.Update(user);
    await context.SaveChangesAsync();
  }

  public async Task<List<DataUser>> Filter(Expression<Func<DataUser, bool>> exp)
  {
    var result = context.DataUsers.Where(exp);
    return await result.ToListAsync();
  }

  public async Task<DataUser?> GetUserByUsername(string username)
  {
    var result = await context.DataUsers.Where(u => u.Username == username).ToListAsync();
    
    if(result.Count < 1)
      return null;
    
    return result.First();
  }
}
