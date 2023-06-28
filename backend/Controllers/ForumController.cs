using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model;

namespace backend.Controllers;

[EnableCors("MainPolicy")]
[ApiController]
[Route("forum")]
public class ForumController : ControllerBase
{
  
}