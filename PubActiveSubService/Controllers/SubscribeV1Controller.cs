using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeV1Controller : ControllerBase {
        // POST: api/Subscribe
        [HttpPost]
        public void Post([FromBody] Models.SubscribeV1 subscribeV1) {
        }
    }
}
