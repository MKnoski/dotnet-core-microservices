using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Http.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        [NonAction]
        public HttpResponseMessage LogException(Exception ex)
        {
            var message = new HttpResponseMessage
            {
                Content = new StringContent(ex.Message), 
                StatusCode = HttpStatusCode.ExpectationFailed
            };
            
            return message;
        }
    }
}