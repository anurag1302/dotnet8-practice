using API.Project.Data;
using API.Project.Stuffs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Serilog;
using System.Xml.Serialization;

namespace API.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMessage _message;
        private readonly IConfiguration _configuration;
        private readonly CountryData _options;
        public ValuesController([FromKeyedServices("scoped")]IMessage message, 
            IConfiguration configuration,
            IOptions<CountryData> options)
        {
            _message = message;
            _configuration = configuration;
            _options = options.Value;

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

        [HttpGet("config")]
        public IActionResult ReadConfig()
        {
            Log.Information("Read from IConfiguration pattern");

            var name = _configuration.GetValue<string>("CountryData:Name");
            var capital = _configuration.GetValue<string>("CountryData:Capital");
            var area = _configuration.GetValue<string>("CountryData:Area");

            return Ok(new
            {
                Name = name, Capital = capital, Area = area
            });
        }

        [HttpGet("config-options")]
        public IActionResult ReadConfigViaIOptiions()
        {
            Log.Information("Read from IOptions pattern");

            var name = _options.Name;
            var capital = _options.Capital;
            var area = _options.Area;

            return Ok(new
            {
                Name = name,
                Capital = capital,
                Area = area
            });
        }

        [HttpGet("athletes")]
        public IActionResult GetAthletes()
        {
            var type = "";
            var headers = HttpContext.Request.Headers;
            var contentType = headers.Where(x => x.Key.ToLower() == "content-type".ToLower()).FirstOrDefault();

            if(contentType.Equals(default(KeyValuePair<string, StringValues>)))
            {
                type = contentType.Value;
            }
            
            var data = AtheleteRepository.GetAthletes();

            return Ok(data);
        }
    }
}
