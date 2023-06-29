using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using backend.Model;
using backend.Repositories;

namespace backend.Controllers;

[ApiController]
[EnableCors("MainPolicy")]
[Route("")]
public class PostsController : ControllerBase
{
  [HttpGet("post/{id}")]
  public async Task<IActionResult> getPostById(
    int id,
    [FromServices] IPostRepository repo
  )
  {
    PostDTO? post = await repo.GetPostById(id);

    if (post == null)
      return StatusCode(404);

    return Ok(post);
  }

  [HttpGet("")]
  public async Task<IEnumerable<PostDTO>> Get([FromServices] IPostRepository repo)
  {
    var response = await repo.GetOrderedPosts();
    return response.Take(20);
  }

  [HttpPost]
  public async Task<ActionResult> Post(
    [FromServices] IPostRepository repo,
    [FromBody] HttpPostDTO postDTO
  )
  {
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
}
