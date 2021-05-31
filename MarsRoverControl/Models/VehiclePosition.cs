using MarsRoverControl.Enums;

namespace MarsRoverControl.Service
{
   public class VehiclePosition
    {
        public Location Location { get; set; }
        public Direction VehicleDirection { get; set; }

        public VehiclePosition Clone()
        {
            return (VehiclePosition)this.MemberwiseClone();
        }
    }
}
