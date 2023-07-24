using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet] public ActionResult GetAll()
        {
            
        }
        [HttpGet("{id}")]
        public ActionResult GetById()
        {

        }
        [HttpPost]
        public ActionResult Post()
        {

        }
        [HttpPut("{id}")]
        public ActionResult Put()
        {

        }
        [HttpDelete("{id}")]
        public ActionResult Delete()
        {

        }
    }
}