using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
   public class VehiclePositionProperty
    {
        public int locationOnTheXAxis { get; set; }
        public int locationOnTheYAxis { get; set; }
        public int vehicleDirectionState { get; set; }
    }
}
