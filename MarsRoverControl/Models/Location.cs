using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location(int locationOnTheXAxis, int locationOnTheYAxis)
        {
            X = locationOnTheXAxis;
            Y = locationOnTheYAxis;
        }
    }
}
