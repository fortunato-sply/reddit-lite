using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using backend.Model;
using backend.Repositories;

namespace backend.Controllers;

[ApiController]
[EnableCors("MainPolicy")]
[Route("post")]
public class PostsController : ControllerBase
{
  [HttpGet("get/feedposts")]
  public async Task<List<PostDTO>> Get([FromServices] IPostRepository repo)
  {
    var response = await repo.GetOrderedPosts();
    return response;
  }

  [HttpPost("send")]
  public async Task<ActionResult> Post(
    [FromBody] HttpPostDTO postDTO,
    [FromServices] IPostRepository repo
  )
  {
    Console.WriteLine("chamou a req");
    Post newPost = new Post
    {
      Content = postDTO.Content,
      CreatedAt = DateTime.Now,
      FkUser = postDTO.UserId,
      FkForum = postDTO.ForumId
    };
    
    await repo.Add(newPost);  
    return Ok();
  }

  [HttpGet("get/forumposts/{id}")]
  public async Task<ActionResult<List<PostDTO>>> GetForumPosts(
    string id,
    [FromServices] IPostRepository repo
  )
  {
    var query = await repo.GetForumPosts(int.Parse(id));
    return query;
  }
}
