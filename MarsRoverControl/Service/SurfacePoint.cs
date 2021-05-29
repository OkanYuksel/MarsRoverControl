using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
    public class SurfacePoint
    {
        public int locationOnTheXAxis { get; }
        public int locationOnTheYAxis { get; }
        public RoverVehicle rover { get; set; }


        public SurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis)
        {
            locationOnTheXAxis = _locationOnTheXAxis;
            locationOnTheYAxis = _locationOnTheYAxis;
        }

        public void PlaceVehicleToPoint(RoverVehicle roverVehicle)
        {
            rover = roverVehicle;
        }


    }
}
