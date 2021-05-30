using MarsRoverControl.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
   public class VehiclePositionProperty
    {
        public Location Location { get; set; }
        public Direction VehicleDirection { get; set; }

        public VehiclePositionProperty Clone()
        {
            return (VehiclePositionProperty)this.MemberwiseClone();
        }
    }
}
