using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Consts
{
    public static class Messages
    {
        public const string START_MESSAGE = "Mars Rover mission started.\nHow large is the vehicle's scanning area?\n(You must enter the width and height with a space between them. For example :5 3)";
        public const string INCORRECT_INPUT_MESSAGE = "You have entered incorrectly, try again.";
        public const string OUTSIDE_SURFACE_AREA_MESSAGE = "The coordinate you want to go is outside the surface area.";
        public const string VEHICLE_CHECK_ON_SURFACE_POINT_MESSAGE = "There is another rover vehicle exist at the coordinate you want to go.";
        public const string UNDEFINED_DIRECTION_MESSAGE = "An undefined direction setted for the vehicle.";
        public const string ENTER_MOVEMENT_COMMANDS_MESSAGE = "\nEnter the movement commands for the rover.\n(You must enter commands without any spaces between them. You can use these commands:L,R,M. For example:MLRLRMLR)";
        public const string ENTER_ROVER_POSITION_MESSAGE = "\nThe rover will be positioned on the surface.\nEnter the coordinates and direction you want to position (valid directions : N, E, S, W), with a space between them.\n(For example :1 3 W)";
        public const string ROVER_HAS_BEEN_REGISTERED_MESSAGE = "This rover vehicle has already been registered.";
        public const string ROVER_SUCCESSFULLY_REGISTERED_MESSAGE = "This rover vehicle registered successfully on the surface.";
        public const string ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE = "Commands worked successfully.\nThe position and direction of the rover vehicle on the surface: ";
        public const string ROVER_COMMANDS_COMPLETED_UNSUCCESSFULLY_MESSAGE = "Commands could not be run. The rover vehicle has returned to its original position.\nThe position and direction of the rover vehicle on the surface: ";
        public const string SURFACE_AREA_NOT_DEFINED_MESSAGE = "The rover cannot be positioned because the surface area is not created.";
        public const string INVALID_SURFACE_WIDTH_PARAMETERS_MESSAGE = "Width parameters must be greater than 0.";
        public const string ANOTHER_ROVER_SPAWN_MESSAGE = "\n\nAnother rover will be spawned.";
    }
}
