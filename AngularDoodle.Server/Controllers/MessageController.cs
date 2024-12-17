using AngularDoodle.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AngularDoodle.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAngularApp")]
    public class MessageController : ControllerBase
    {
        [HttpGet(Name = "GetMessage")]
        public Message Get()
        {
            return new Message { Text = "Hello from the server" };
        }
    }
}
