namespace Rover.Service.Business
{
    public class RoverEventData
    {
        public Point PreviousLocation { get; set; }
        public Direction PreviousDirection { get; set; }
        public Rover Rover { get; set; }
    }
}
