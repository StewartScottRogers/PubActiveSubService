using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeListChannelsV1Controller : ControllerBase {
        // GET: api/PrototypeListChannels
        [HttpGet]
        public Models.ListChannelsV1 Get() =>
             new Models.ListChannelsV1 { ChannelSearch = "*" };
    }
}
