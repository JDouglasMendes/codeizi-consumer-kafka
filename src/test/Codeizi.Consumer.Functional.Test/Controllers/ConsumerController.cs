using Microsoft.AspNetCore.Mvc;

namespace Codeizi.Consumer.Functional.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly InMemoryDatabase database;
        public ConsumerController(InMemoryDatabase database)
            => this.database = database;

        [HttpGet]
        public IActionResult Get()
            => Ok(database.Message);
    }
}