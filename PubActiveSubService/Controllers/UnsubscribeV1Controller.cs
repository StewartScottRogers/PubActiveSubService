using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UnsubscribeV1Controller : ControllerBase {
        // POST: api/Unsubscribe
        [HttpPost]
        public void Post([FromBody] Models.UnsubscribeV1 unsubscribeV1) {

        }
    }
}
