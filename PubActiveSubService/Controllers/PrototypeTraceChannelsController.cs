using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeTraceChannelsController : ControllerBase
    {
        // GET: api/PrototypeTraceChannels
        [HttpGet]
        public Models.TraceChannelsV1 Get()
        {
            return new Models.TraceChannelsV1 {  ChannelSearch = "*" };
        }
    }
}
