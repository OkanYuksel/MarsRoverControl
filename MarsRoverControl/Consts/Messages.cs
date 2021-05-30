using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Consts
{
    public static class Messages
    {
        public const string START_MESSAGE = "Mars Rover mission started.\nHow large is the vehicle's scanning area?";
        public const string WRONG_INPUT = "You have entered incorrectly, try again.";
        public const string OUTSIDE_SURFACE_AREA_MESSAGE = "The coordinate you want to go is outside the surface area.";
        public const string DIFFERENT_VEHICLE_EXIST_ON_SURFACE_POINT = "There is another rover vehicle at the coordinate you want to go.";
        public const string UNDEFINED_DIRECTION_MESSAGE = "An undefined direction setted for the vehicle.";
        public const string ENTER_MOVEMENT_COMMANDS_MESSAGE = "Enter the movement commands for the rover.";
        public const string ENTER_ROVER_POSITION_MESSAGE = "The rover will be positioned on the surface. Enter your parameters.";

    }
}
