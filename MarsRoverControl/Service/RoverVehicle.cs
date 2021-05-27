﻿using MarsRoverControl.Enums;
using MarsRoverControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
    public class RoverVehicle : IRoverVehicle
    {
        private int locationOnTheXAxis { get; set; }
        private int locationOnTheYAxis { get; set; }
        private int vehicleDirectionState { get; set; }

        public RoverVehicle(int _locationOnTheXAxis, int _locationOnTheYAxis, int _vehicleDirectionState)
        {
            locationOnTheXAxis = _locationOnTheXAxis;
            locationOnTheYAxis = _locationOnTheYAxis;
            vehicleDirectionState = _vehicleDirectionState;
        }

        public string TurnLeft()
        {
            if (vehicleDirectionState > 0)
            {
                vehicleDirectionState -= 1;
            }
            else
            {
                vehicleDirectionState = GetDirectionCount() - 1;
            }
            return GetRoverPositionAndDirection();
        }

        public string TurnRight()
        {
            vehicleDirectionState = (vehicleDirectionState + 1) % GetDirectionCount();
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
            return GetRoverPositionOnSurface() + " " + Enum.GetName(typeof(RoverDirections), vehicleDirectionState);
        }

        public string GetRoverPositionOnSurface()
        {
            return locationOnTheXAxis + " " + locationOnTheYAxis;
        }


    }
}
