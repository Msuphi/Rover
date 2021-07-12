using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Service.Business.CQRS.Commands.Response
{
    public class RunRoverCommandResponse
    {
        public StringBuilder Results { get; set; }
        public RunRoverCommandResponse()
        {
            Results = new StringBuilder();
        }
    }
}
