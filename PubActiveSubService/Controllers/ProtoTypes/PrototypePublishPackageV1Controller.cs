using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypePublishPackageV1Controller : ControllerBase {
        // GET: api/PrototypePublishPackageV1
        [HttpGet]
        public Models.PublishPackageV1 Get() =>
             new Models.PublishPackageV1() {
                 Channel = "Channel/Test",
                 Package = $"Test Package. Some String data that will be posted on to some numbers of Subscribers ID:{System.DateTime.UtcNow.Ticks:000000000000000}."
             };
    }
}
