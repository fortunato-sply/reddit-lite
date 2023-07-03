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

      DataUser user;
      try
      {
        user = await userService.ValidateUserToken(new Jwt { Value = jwt });
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      Console.WriteLine("passou");

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

      user.Photo = imgId;
      await userRepo.Update(user);

      return Ok();
    }
}
