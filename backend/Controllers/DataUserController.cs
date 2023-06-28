using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model;

namespace backend.Controllers;

[EnableCors("MainPolicy")]
[ApiController]
[Route("user")]
public class DataUserController : ControllerBase
{
    private RedditliteContext context;
    public DataUserController(RedditliteContext context) => this.context = context;

    [HttpGet("{id}")]
    public DataUser? getUserById(int id)
    {
      DataUser? user = context.DataUsers.FirstOrDefault(x => x.Id == id);
      return user;
    }
}
