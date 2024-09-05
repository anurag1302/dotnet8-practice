using API.Project.Stuffs;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMessage _message;
        public ValuesController([FromKeyedServices("scoped")]IMessage message)
        {
            _message = message;
        }

        [HttpGet("message")]
        public IActionResult GetValues()
        {
            Log.Information("/message endpoint hit");

            var message = _message.GetMessage();
            return Ok(message);
        }

        [HttpGet("divide")]
        public IActionResult Divide()
        {
            var den = 0;
            var num = 1;

            var abc = num / den;
            return Ok(abc);
        }
    }
}
