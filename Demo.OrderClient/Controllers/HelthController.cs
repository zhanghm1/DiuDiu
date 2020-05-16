using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.OrderApi
{
    [ApiController]
    [Route("[controller]")]
    public class HelthController : ControllerBase
    {
       
        private readonly ILogger<HelthController> _logger;

        public HelthController(ILogger<HelthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //Thread.Sleep(4000);
            Console.WriteLine("Helth Check");
            return Ok();
        }
    }
}
