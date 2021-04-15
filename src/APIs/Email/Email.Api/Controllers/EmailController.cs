using System.Threading.Tasks;
using AutoMapper;
using Email.Api.Commands;
using Email.Domain.Models;
using Infrastructure.Controllers;
using Infrastructure.HttpModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Email.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public EmailController(ILogger<EmailController> logger, IMediator mediator, IMapper mapper) : base(logger)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailHttpModel sendEmailHttpModel)
        {
            var sendEmail = _mapper.Map<SendEmailModel>(sendEmailHttpModel);

            var command = new SendEmailCommand(sendEmail);

            await _mediator.Send(command);
            
            return Ok();
        }
    }
}
