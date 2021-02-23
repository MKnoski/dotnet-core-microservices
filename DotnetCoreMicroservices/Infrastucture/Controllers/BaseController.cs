using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Infrastucture.Controllers
{
    public class BaseController : ControllerBase
    {
        private ILogger _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        public ILogger Logger { get { return _logger; } }

        [NonAction]
        public HttpResponseMessage LogException(Exception ex)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            message.Content = new StringContent(ex.Message);
            message.StatusCode = System.Net.HttpStatusCode.ExpectationFailed;
            return message;
        }
    }
}