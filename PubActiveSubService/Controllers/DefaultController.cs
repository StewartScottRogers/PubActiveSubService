using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    public class DefaultController : ControllerBase {

        public DefaultController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi() {
            RecordHostUrl();
            return Redirect("/swagger/");
        }
    }
}