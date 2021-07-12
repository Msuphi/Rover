using MediatR;
using Rover.Service.Business.CQRS.Commands.Response;
using System.Collections.Generic;

namespace Rover.Service.Business.CQRS.Commands.Requests
{
    public class RunRoverCommandRequest : IRequest<RunRoverCommandResponse>
    {
        public List<string> Commands { get; set; }
    }
}
