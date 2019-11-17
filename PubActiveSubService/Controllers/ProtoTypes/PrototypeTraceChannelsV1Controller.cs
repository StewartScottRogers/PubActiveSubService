using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeTraceChannelsV1Controller : ControllerBase {
        // GET: api/PrototypeTraceChannels
        [HttpGet]
        public Models.TraceChannelsV1 Get() =>
             new Models.TraceChannelsV1 { ChannelSearch = "*" };
    }
}
