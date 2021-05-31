using MarsRoverControl.Service;

namespace MarsRoverControl.Models
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public VehiclePosition VehiclePosition { get; set; }
    }
}
