using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeUnsubscribeV1Controller : ControllerBase {
        // GET: api/PrototypeUnsubscribe
        [HttpGet]
        public Models.UnsubscribeV1 Get() =>
             new Models.UnsubscribeV1();
    }
}
