using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase {
        private readonly IIntegrationProcessors PubActiveSubServiceProcessors;

        public PublishController(IIntegrationProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public string Post([FromBody] Models.PublishPackage publishPackage) {
            PubActiveSubServiceProcessors.SaveHostUrl($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.Publish(publishPackage); ;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Get() {
            return PubActiveSubServiceProcessors.Ping();
        }
    }
}
