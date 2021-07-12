using MediatR;
using Rover.Service.Business.CQRS.Commands.Requests;
using Rover.Service.Business.CQRS.Commands.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Rover.Service.Business.CQRS.Handlers.CommandHandlers
{
    public class RunRoverCommandHandler : IRequestHandler<RunRoverCommandRequest, RunRoverCommandResponse>
    {
        public async Task<RunRoverCommandResponse> Handle(RunRoverCommandRequest request, CancellationToken cancellationToken)
        {
            string commands = string.Join('\n', request.Commands);
            var result = new Discovery().Run(commands);
            var runRoverCommandResponse = new RunRoverCommandResponse();
            
            for (int i = 0; i < result.Count; i++)
            {
                Rover currentRobot = result[i];
                runRoverCommandResponse.Results.AppendLine(currentRobot.Location.X.ToString());
                runRoverCommandResponse.Results.AppendLine(currentRobot.Location.Y.ToString());
                runRoverCommandResponse.Results.AppendLine(currentRobot.Direction.ToString()[0].ToString());
                runRoverCommandResponse.Results.AppendLine(currentRobot.IsCrashed ? "Crashed :@" : (currentRobot.IsLost ? "Lost :(" : ""));
            }

            
            return runRoverCommandResponse;
        }
    }
}
