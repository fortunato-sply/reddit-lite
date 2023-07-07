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

  private Task<List<PostDTO>> getCompletePosts()
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
        Photo = u.Photo,
        AuthorName = u.Username,
        Content = p.Content,
        CreatedAt = p.CreatedAt,
        Likes = likesGroup.Sum(l => l.Value)
      };

    return response.ToListAsync();
  }

  public async Task<List<PostDTO>> GetOrderedPosts()
  {
    var response = await getCompletePosts();
    return response.OrderBy(p => p.CreatedAt).ToList();
  }

  public async Task<List<PostDTO>> GetForumPosts(int forumId)
  {
    var query = 
      from p in context.Posts
      join f in context.Forums
        on p.FkForum equals f.Id
      join u in context.DataUsers
        on p.FkUser equals u.Id
      join l in context.Likes
        on p.Id equals l.FkPost into likesGroup
      where f.Id == forumId
      select new PostDTO
      {
        AuthorName = u.Username,
        Photo = u.Photo,
        ForumName = f.Title,
        Content = p.Content,
        CreatedAt = p.CreatedAt,
        IdAuthor = u.Id,
        Likes = likesGroup.Sum(l => l.Value) 
      };

      return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
  }
}
