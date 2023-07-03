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
    
    return Ok(forums.First());
  }

  [HttpGet("myforums/{userId}")]
  public async Task<ActionResult<List<ForumDTO>>> GetUserForums(
    int userId,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.GetUserForums(userId);

    if(forums.Count < 1)
      return StatusCode(404);

    return Ok(forums);
  }

  [HttpGet("favorites/{userId}")]
  public async Task<ActionResult<List<ForumDTO>>> GetUserFavoriteForums(
    int userId,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.GetUserFavoriteForums(userId);

    if(forums.Count < 1)
      return StatusCode(404);
      
    return Ok(forums);
  }

  [HttpPost("newforum")]
  public async Task<IActionResult> Create(
    [FromForm] CreateForumDTO model,
    [FromServices] IForumRepository repo,
    [FromServices] IImageService imageService
  )
  {
    Forum forum = new Forum
    {
      Title = model.Title,
      Description = model.Description,
      CreatedAt = new DateTime()
    };
    
    var id = await imageService.GetLastImageId();
    forum.Photo = id;

    return Ok();
  }

}