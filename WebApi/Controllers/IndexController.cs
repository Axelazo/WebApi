using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public ContentResult Index()
        {
            return Content("<div>Api Web RESTful, Desarrollo Web!</div>", "text/html");
        }
    }
}
