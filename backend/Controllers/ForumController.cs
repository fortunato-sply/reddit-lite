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
  public async Task<ActionResult<List<Forum>>> SearchForumByName(
    string name,
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.Filter(f => f.Title.Contains(name));

    if (forums.Count < 1)
      return BadRequest();
    
    return Ok(forums.ToList());
  }

  [HttpGet("getforums")]
  public async Task<ActionResult<List<ForumDTO>>> GetForums(
    [FromServices] IForumRepository repo
  )
  {
    var forums = await repo.GetForums();
    return Ok(forums);
  }

  [HttpGet("myforums")]
  public async Task<ActionResult<List<ForumDTO>>> GetUserForums(
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    var jwt = Request.Headers["jwt"];
    Console.WriteLine(jwt);
    UserToken user = await userService.ValidateUserToken(new Jwt { Value = jwt });
    var forums = await repo.GetUserForums(user.Id);
    if(forums.Count < 1)
      return Ok(new List<ForumDTO>(0));

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

  [HttpPost("create")]
  public async Task<ActionResult<int>> Create(
    [FromBody] CreateForumDTO data,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    var query = await repo.Filter(f => f.Title == data.Title);
    if (query.Count > 0)
      return BadRequest("Fórum já existente. Utilize outro nome.");

    UserToken user = await userService.ValidateUserToken(new Jwt { Value = data.JwtToken });

    Forum forum = new Forum
    {
      Title = data.Title,
      Description = data.Description,
      CreatedAt = DateTime.Now,
      Owner = user.Id
    };
    
    await repo.Add(forum);

    var id = await repo.GetLastForumID();
    await repo.AddRelationship(id, user.Id);

    return Ok(id);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ForumDTO>> GetForumByID(
    int id,
    [FromServices] IForumRepository repo
  )
  {
    var forum = await repo.GetForumDTOByID(id);

    if (forum == null)
      return BadRequest("fórum não encontrado");

    return Ok(forum);
  }

  [HttpPost("del/{id}")]
  public async Task<ActionResult> DeleteForum(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository forumRepo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);

    Forum forum = await forumRepo.GetForumByID(int.Parse(id));
    
    if (forum == null)
      return BadRequest("o fórum não existe");
     
    if (user.Id != forum.Owner)
      return BadRequest("o usuário não é proprietário do fórum em questão.");
    
    await forumRepo.Delete(forum);
    return Ok();
  }

  [HttpPost("checkfollow/{id}")]
  public async Task<ActionResult<bool>> IsUserFollowingForum(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);

    var query = await repo.IsUserFollowingForum(user.Id, int.Parse(id));
    return query;
  }

  [HttpPost("startfollow/{id}")]
  public async Task<ActionResult> StartFollowingForum(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);

    await repo.StartFollowingForum(user.Id, int.Parse(id));
    return Ok();
  }

  [HttpPost("stopfollow/{id}")]
  public async Task<ActionResult> StopFollowingForum(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);
 
    await repo.StopFollowingForum(user.Id, int.Parse(id));
    return Ok();
  }

  [HttpPost("checkfavorite/{id}")]
  public async Task<ActionResult<bool>> IsForumFavorite(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);

    var response = await repo.IsForumFavorite(user.Id, int.Parse(id));
    return response;
  }

  [HttpPost("favorite/{id}")]
  public async Task<ActionResult> FavoriteForum(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);

    await repo.FavoriteForum(user.Id, int.Parse(id));
    return Ok();
  }

  [HttpPost("unfavorite/{id}")]
  public async Task<ActionResult> UnfavoriteForum(
    string id,
    [FromBody] Jwt jwt,
    [FromServices] IForumRepository repo,
    [FromServices] IUserService userService
  )
  {
    UserToken user = await userService.ValidateUserToken(jwt);

    await repo.UnfavoriteForum(user.Id, int.Parse(id));
    return Ok();
  }

  [HttpPost("update/{id}")]
  public async Task<ActionResult> UpdateForum(
    string id,
    [FromBody] UpdateForumDTO data,
    [FromServices] IForumRepository repo
  )
  {
    var forum = await repo.GetForumByID(int.Parse(id));

    if(forum is null)
      return NotFound();

    forum.Title = data.Title;
    forum.Description = data.Description;

    await repo.Update(forum);
    return Ok();
  }

  [HttpGet("members/{id}")]
  public async Task<ActionResult<List<UserMemberDTO>>> GetForumMembers(
    string id,
    [FromServices] IForumRepository repo
  )
  {
    return await repo.GetMembers(int.Parse(id));
  }
}