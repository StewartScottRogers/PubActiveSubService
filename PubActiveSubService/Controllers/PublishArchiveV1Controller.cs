using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PublishArchiveV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public PublishArchiveV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public string Post([FromBody] Models.PublishPackageV1 publishPackageV1) {
            PubActiveSubServiceProcessors.SaveArchiveHostDns($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.PublishArchive(publishPackageV1);
        }
    }
}
