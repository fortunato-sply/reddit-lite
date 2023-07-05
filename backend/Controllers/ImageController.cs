using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model;
using backend.Repositories;
using backend.Services;
using Security.Jwt;

namespace backend.Controllers;

[EnableCors("MainPolicy")]
[ApiController]
[Route("img")]
public class ImageController : ControllerBase
{
    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<ActionResult<string>> Post(
      [FromServices] IImageService repo
    )
    {
      var files = Request.Form.Files;

      if(files is null || files.Count == 0)
        return BadRequest("files is null or count == 0");

      var file = Request.Form.Files[0];

      if (file.Length < 1)
        return BadRequest("file is null");

      await repo.AddImage(file);

      var photoId = await repo.GetLastImageId();
      return Ok(photoId);
    }

    [HttpPost("user/update")]
    public async Task<ActionResult> UpdateUserImage(
      [FromServices] IImageService imageService,
      [FromServices] IUserRepository userRepo,
      [FromServices] IUserService userService
    )
    {
      var jwt = Request.Form["jwt"].ToString();
      Console.WriteLine("jwt: " + jwt);

      UserToken user;
      try
      {
        user = await userService.ValidateUserToken(new Jwt { Value = jwt });
        Console.WriteLine(user);
      }
      catch (Exception ex)
      {
        Console.WriteLine("aqui " + ex.Message);
        return BadRequest(ex.Message);
      }


      if (user is null)
        return NotFound();

      var files = Request.Form.Files;

      if (files is null || files.Count == 0)
        return BadRequest();
      
      var file = Request.Form.Files[0];

      if (file.Length < 1)
        return BadRequest();
      
      await imageService.AddImage(file);
      var imgId = await imageService.GetLastImageId();

      var userData = await userRepo.GetUserByUsername(user.Username); 
      userData.Photo = imgId;
      await userRepo.Update(userData);

      return Ok();
    }

    [HttpPost("forum/update")]
    public async Task<ActionResult> UpdateForumImage(
      [FromServices] IImageService imageService,
      [FromServices] IForumRepository forumRepo
    )
    {
      var id = int.Parse(Request.Form["id"]); 
      Console.WriteLine("forum id: " + id);

      Forum forum;
      try
      {
        forum = await forumRepo.GetForumByID(id);
        Console.WriteLine("forum: " + forum);
      }
      catch (Exception ex)
      {
        Console.WriteLine("erro puxando o forum por id: " + ex.Message);
        return BadRequest(ex.Message);
      }


      if (forum is null)
        return NotFound();

      var files = Request.Form.Files;

      if (files is null || files.Count == 0)
        return BadRequest();
      
      var file = Request.Form.Files[0];

      if (file.Length < 1)
        return BadRequest();
      
      await imageService.AddImage(file);
      var imgId = await imageService.GetLastImageId();

      
      forum.Photo = imgId;
      await forumRepo.Update(forum);

      return Ok();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult> getImageById(
      string code,
      [FromServices] IImageService imageService
    )
    {
      if (int.TryParse(code, out int id))
      {
        var query = await imageService.Filter(i => i.Id == id);
        var img = query.FirstOrDefault();

        if (img is null)
          return NotFound();
        
        return File(img.Photo, "image/jpeg");
      }

      return BadRequest("code needs to be an integer");
    }
}
