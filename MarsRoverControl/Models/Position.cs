using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int locationOnTheXAxis, int locationOnTheYAxis)
        {
            X = locationOnTheXAxis;
            Y = locationOnTheYAxis;
        }
    }
}
