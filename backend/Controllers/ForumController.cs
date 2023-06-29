using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model;
using backend.Repositories;

namespace backend.Controllers;

[EnableCors("MainPolicy")]
[ApiController]
[Route("forum")]
public class ForumController : ControllerBase
{
  [HttpGet("search/{name}")]
  public async Task<ActionResult<Forum>> SearchForumByName(
    string name,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.Filter(f => f.Title.Contains(name));

    if (forums.Count < 1)
      return BadRequest();
    
    return Ok(forums);
  }

  [HttpGet("myforums/{userId}")]
  public async Task<ActionResult<ForumDTO>> GetUserForums(
    int userId,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.GetUserForums(userId);

    return Ok(forums);
  }
}