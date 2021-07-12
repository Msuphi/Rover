using System;
using System.Collections.Generic;
using System.Linq;

namespace Rover.Service.Business
{
    public class Rover
    {
        public event Action<RoverEventData> CommandExecuted;
        public bool IsCrashed { get; set; }
        public bool IsLost { get; set; }
        public Point Location { get; set; }
        public Direction Direction { get; set; }
        public Queue<Action<Rover>> Commands { get; set; }

        public Rover()
        {
            Location = new Point(0, 0);
            Commands = new Queue<Action<Rover>>();
        }

        public void ExecuteCommands()
        {
            while (Commands.Count() > 0 && !IsCrashed && !IsLost)
            {
                Point previousLocation = this.Location.Clone();
                Direction previousDirection = this.Direction;
                Commands.Dequeue().Invoke(this);

                if (CommandExecuted != null)
                    CommandExecuted.Invoke(new RoverEventData { PreviousLocation = previousLocation, PreviousDirection = previousDirection, Rover = this });
            }
        }
    }
}
