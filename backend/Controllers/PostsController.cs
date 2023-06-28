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
    public IActionResult getPostById(
      int id,
      IPostRepository repo
    )
    {
      PostDTO? post = repo.GetPostById(id);
      
      if(post == null)
        return StatusCode(404);
      
      return Ok(post);
    }

    [HttpGet("")]
    public IEnumerable<PostDTO> Get(
      IPostRepository repo
    ) 
      => repo.GetOrderedPosts().Take(10);
}
