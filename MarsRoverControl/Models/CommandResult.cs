using MarsRoverControl.Service;

namespace MarsRoverControl.Models
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public VehiclePositionProperty VehiclePosition { get; set; }
    }
}
