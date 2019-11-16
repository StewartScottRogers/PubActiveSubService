using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeListChannelsController : ControllerBase
    {
        // GET: api/PrototypeListChannels
        [HttpGet]
        public Models.ListChannelsV1 Get()
        {
            return new Models.ListChannelsV1 {  ChannelSearch = "*" };
        }
    }
}
