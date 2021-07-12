using MediatR;
using Moq;
using Rover.Service.Business.CQRS.Commands.Requests;
using Rover.Service.Business.CQRS.Handlers.CommandHandlers;
using System;
using Xunit;

namespace RoverTest
{
    public class DiscoveryUnitTest
    {
        [Fact]
        public void RoverDiscovers()
        {
            var mediator = new Mock<IMediator>();
            RunRoverCommandRequest command = new RunRoverCommandRequest();
            command.Commands = new System.Collections.Generic.List<string> {
            "20 20",
            "0 0 E",
            "MMMMMLMMMMMRMMMMMLMMMMM",
            "5 9 W",
            "MMLMMMRMMMMRMMMRMMMM"
            };

            RunRoverCommandHandler handler = new RunRoverCommandHandler();
            var runRoverCommandResponse =  handler.Handle(command, new System.Threading.CancellationToken()).GetAwaiter().GetResult();

            Assert.NotEmpty(runRoverCommandResponse.Results.ToString());
        } 
    }
}
