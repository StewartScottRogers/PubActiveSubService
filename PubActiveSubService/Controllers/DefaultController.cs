﻿using Microsoft.AspNetCore.Mvc;

namespace PubActiveSubService.Controllers {
    public class DefaultController : Controller
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi() {
            return Redirect("/swagger/");
        }
    }
}