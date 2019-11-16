using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    [ApiController]
    public class PublishV1Controller : ControllerBase {

        // POST api/Publish
        [Route("api/[controller]")]
        [HttpPost]
        public void Post([FromBody] Models.PublishPackageV1 publishPackageV1) {

        }

    }
}
