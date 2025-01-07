using AngularDoodle.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AngularDoodle.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAngularApp")]
    // A controller that returns a message from the server
    // Currently, used to test the connection between the Angular app and the server
    public class MessageController : ControllerBase
    {
        [HttpGet(Name = "GetMessage")]
        // Simple GET request that returns a message object
        public Message Get()
        {
            return new Message { Text = "Hello from the server" };
        }
    }
}
