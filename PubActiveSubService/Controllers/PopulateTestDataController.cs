using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {


#if DEBUG
    [Route("api/[controller]")]
    [ApiController]
    public class PopulateTestDataController : ControllerBase {
        public PopulateTestDataController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpGet]
        public string Get() {
            return IntegrationProcessors.PopulateTestData(base.RecordHostUrl());
        }
    }
#endif



}