using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rover.Service.Business.CQRS.Commands.Requests;
using Rover.Service.Business.CQRS.Commands.Response;
using System.Threading.Tasks;

namespace Rover.Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoverController : ControllerBase
    {
        IMediator _mediator;
        public RoverController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RunRoverCommandRequest requestModel)
        {
            RunRoverCommandResponse response = await _mediator.Send(requestModel);
            return Ok(response.Results.ToString());
        }
    }
}
