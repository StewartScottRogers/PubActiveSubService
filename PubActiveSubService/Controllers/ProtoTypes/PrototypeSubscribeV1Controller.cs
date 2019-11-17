using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeSubscribeV1Controller : ControllerBase {
        // GET: api/PrototypeSubscribe
        [HttpGet]
        public Models.SubscribeV1 Get() =>
             new Models.SubscribeV1();
    }
}
