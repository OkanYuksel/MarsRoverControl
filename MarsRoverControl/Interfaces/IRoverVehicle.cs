using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface IRoverVehicle
    {
        public string TurnLeft();
        public string TurnRight();
        public string MoveForward();
        public int GetDirectionCount();
        public string GetRoverPositionAndDirection();
        public string GetRoverPositionOnSurface();
    }
}
