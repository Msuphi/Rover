using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rover.Service.Business
{
    public class Simulation
    {
        public Point GridSize { get; set; }
        public List<Rover> RoverCollection { get; private set; }

        public Simulation()
        {
            RoverCollection = new List<Rover>();
        }

        public void Run()
        {
            foreach (var rover in RoverCollection)
            {
                rover.ExecuteCommands();
                if (rover.IsCrashed || rover.IsLost)
                {

                    Console.WriteLine("CRASHED !!");
                    break;
                }
            }
        }
    }
}
