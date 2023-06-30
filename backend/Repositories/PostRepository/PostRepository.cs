namespace backend.Repositories;

using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

public class PostRepository : IPostRepository
{
  private RedditliteContext context;
  public PostRepository(RedditliteContext context)
    => this.context = context;

  public async Task Save()
   => await this.context.SaveChangesAsync();

  public async Task Add(Post post)
  {
    await context.Posts.AddAsync(post);
    await context.SaveChangesAsync();
  }

  public async Task Delete(Post post)
  {
    context.Posts.Remove(post);
    await context.SaveChangesAsync();
  }

  public async Task Update(Post post)
  {
    context.Posts.Update(post);
    await context.SaveChangesAsync();
  }

  public async Task<List<Post>> Filter(Expression<Func<Post, bool>> exp)
  {
    var result = context.Posts.Where(exp);
    return await result.ToListAsync();
  }

  private IQueryable<PostDTO> getCompletePosts()
  {
    var response =
      from p in context.Posts
      join u in context.DataUsers
        on p.FkUser equals u.Id
      join c in context.Comments
        on p.Id equals c.FkPost into commentsGroup
      join f in context.Forums
        on p.FkForum equals f.Id
      join l in context.Likes
        on p.Id equals l.FkPost into likesGroup
      select new PostDTO
      {
        IdAuthor = u.Id,
        AuthorName = u.Username,
        Content = p.Content,
        CreatedAt = p.CreatedAt,
        Comments = commentsGroup.ToList(), // puxar os comentários e convertê-los em uma lista
        Likes = likesGroup.Sum(l => l.Value),
        IdForum = f.Id,
        ForumName = f.Title
      };

    return response;
  }

  public async Task<IEnumerable<PostDTO>> GetOrderedPosts()
  {
    var response = getCompletePosts().OrderBy(p => p.CreatedAt);
    return await response.ToListAsync();
  }


  public async Task<PostDTO?> GetPostById(int id)
  {
    var postResponse = 
      from p in context.Posts
      where p.Id == id
      join u in context.DataUsers
        on p.FkUser equals u.Id
      join c in context.Comments
        on p.Id equals c.FkPost into commentsGroup
      join f in context.Forums
        on p.FkForum equals f.Id
      select new PostDTO                 
      {
        IdPost = p.Id,
        IdAuthor = u.Id,
        AuthorName = u.Username,
        Content = p.Content,
        CreatedAt = p.CreatedAt,
        Comments = commentsGroup.ToList(), // puxar os comentários e convertê-los em uma lista
        IdForum = f.Id,
        ForumName = f.Title
      };

    return await postResponse.FirstOrDefaultAsync();
  }
}
