using System;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        // GET
        [HttpGet]
        [Route("Health")]
        public ActionResult<string> Health()
        {
            return "Status ok - " + DateTime.UtcNow.ToLongDateString();
        }
    }
}