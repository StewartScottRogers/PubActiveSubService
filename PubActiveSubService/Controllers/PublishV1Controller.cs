using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [ApiController]
    public class PublishV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public PublishV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        // POST api/Publish
        [Route("api/[controller]")]
        [HttpPost]
        public string Post([FromBody] Models.PublishPackageV1 publishPackageV1) {
            PubActiveSubServiceProcessors.SaveArchiveHost($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.Publish(publishPackageV1); ;
        }       
    }
}
