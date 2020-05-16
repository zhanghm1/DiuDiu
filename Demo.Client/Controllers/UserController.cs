using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.UserApi
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
       
        private readonly ILogger<UserController> _logger;
        private static List<User> users = new List<User>() {
            new User(){Name="张三" },
            new User(){Name="李四" },
        };

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<User> Get()
        {
            return users;
        }
        [HttpPost]
        public List<User> Post(User user)
        {
            users.Add(user);
            return users;
        }
    }


    public class User
    { 
        public string Name { get; set; }
    }
}
