using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
   public class VehiclePositionProperty
    {
        public Position PositionOnSurface { get; set; }
        public int VehicleDirectionState { get; set; }

        public VehiclePositionProperty Clone()
        {
            return (VehiclePositionProperty)this.MemberwiseClone();
        }
    }
}
