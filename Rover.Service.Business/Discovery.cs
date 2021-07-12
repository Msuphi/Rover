using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Rover.Service.Business
{
    public class Discovery
    {
        private readonly string _commandRegexPattern = "^(?<gridSize>[0-9]+ [0-9]+)(?<rover>\n[0-9]+ [0-9]+ [NEWS]{1}\n[MRL]+)+$";

        public List<Rover> Run(string _command)
        {
            if (string.IsNullOrEmpty(_command))
            {
                Console.WriteLine("There is no command to run.");
                return new List<Rover>();
            }
            else
            {
                Match match = Regex.Match(_command, _commandRegexPattern);
                if (match.Success)
                {
                    Simulation simulation = new Simulation();

                    string[] gridSizeParams = match.Groups["gridSize"].Value.Split(' ');
                    int xMax, yMax;
                    if (!int.TryParse(gridSizeParams[0], out xMax) || !int.TryParse(gridSizeParams[1], out yMax))
                    {
                        Console.WriteLine("Grid size is too large or too small.");
                        return new List<Rover>();
                    }

                    simulation.GridSize = new Point(xMax, yMax);

                    foreach (var item in match.Groups["rover"].Captures)
                    {
                        Capture capture = item as Capture;
                        string[] paramsLineArray = capture.Value.Split('\n');
                        string[] locationParams = paramsLineArray[1].Split(' ');
                        char[] commandLetters = paramsLineArray[2].ToCharArray();

                        int x, y;
                        if (!int.TryParse(locationParams[0], out x) || !int.TryParse(locationParams[1], out y))
                        {
                            Console.WriteLine("Robot location is out of range.");
                            return new List<Rover>(); ;
                        }

                        Rover rover = new Rover
                        {
                            Location = new Point(x, y),
                            Direction = locationParams[2] == "N" ? Direction.North : (locationParams[2] == "E" ? Direction.East : (locationParams[2] == "W" ? Direction.West : Direction.South))
                        };

                        commandLetters.ToList().ForEach(c => rover.Commands.Enqueue(c == 'M' ? Move : (c == 'L' ? RotateLeft : new Action<Rover>(RotateRight))));

                        simulation.RoverCollection.Add(rover);
                    }

                    simulation.Run();

                    return simulation.RoverCollection;

                }
                else
                {
                    Console.WriteLine("Please enter a valid input command.");
                    return new List<Rover>();
                }
            }
        }
        private void Move(Rover rover)
        {
            switch (rover.Direction)
            {
                case Direction.East:
                    rover.Location.X += 1;
                    break;
                case Direction.West:
                    rover.Location.X -= 1;
                    break;
                case Direction.North:
                    rover.Location.Y += 1;
                    break;
                case Direction.South:
                    rover.Location.Y -= 1;
                    break;
            }
        }
        private void RotateLeft(Rover rover)
        {
            rover.Direction = (Direction)(((int)rover.Direction + 5) % 4);
        }
        private void RotateRight(Rover rover)
        {
            rover.Direction = (Direction)(((int)rover.Direction + 3) % 4);
        }
    }
}
