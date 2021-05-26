using MarsRoverControl.Enums;
using MarsRoverControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Models
{
    public class RoverVehicle : IRoverVehicle
    {
        private int LocationOnTheXAxis { get; set; }
        private int LocationOnTheYAxis { get; set; }
        private int VehicleDirectionState { get; set; }

        public RoverVehicle(int locationOnTheXAxis, int locationOnTheYAxis, int vehicleDirectionState)
        {
            LocationOnTheXAxis = locationOnTheXAxis;
            LocationOnTheYAxis = locationOnTheYAxis;
            VehicleDirectionState = vehicleDirectionState;
        }

        public string TurnLeft()
        {
            if (VehicleDirectionState > 0)
            {
                VehicleDirectionState -= 1;
            }
            else
            {
                VehicleDirectionState = GetDirectionCount() - 1;
            }
            return GetRoverPositionAndDirection();
        }

        public string TurnRight()
        {
            VehicleDirectionState = (VehicleDirectionState + 1) % GetDirectionCount();
            return GetRoverPositionAndDirection();
        }

        public string MoveForward()
        {
            return GetRoverPositionAndDirection();
        }

        public int GetDirectionCount()
        {
            return Enum.GetNames(typeof(RoverDirections)).Length;
        }

        public string GetRoverPositionAndDirection()
        {
            return GetRoverPositionOnSurface() + " " + Enum.GetName(typeof(RoverDirections), VehicleDirectionState);
        }

        public string GetRoverPositionOnSurface()
        {
            return LocationOnTheXAxis + " " + LocationOnTheYAxis;
        }


    }
}
