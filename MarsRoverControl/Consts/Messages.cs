using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Consts
{
    public static class Messages
    {
        public const string StartMessage = "Mars Rover mission started.\nHow large is the vehicle's scanning area?\n(You must enter the width and height with a space between them. For example :5 3)";
        public const string IncorrectInputMessage = "You have entered incorrectly, try again.";
        public const string OutsideSurfaceAreaMessage = "The coordinate you want to go is outside the surface area.";
        public const string VehicleCheckOnSurfacePointMessage = "There is another rover vehicle exist at the coordinate you want to go.";
        public const string UndefinedDirectionMessage = "An undefined direction setted for the vehicle.";
        public const string EnterMovementCommandsMessage = "\nEnter the movement commands for the rover.\n(You must enter commands without any spaces between them. You can use these commands:L,R,M. For example:MLRLRMLR)";
        public const string EnterRoverPositionMesage = "\nThe rover will be positioned on the surface.\nEnter the coordinates and direction you want to position (valid directions : N, E, S, W), with a space between them.\n(For example :1 3 W)";
        public const string RoverHasBeenRegisteredMessage = "This rover vehicle has already been registered.";
        public const string RoverSuccessfullyRegisteredMessage = "This rover vehicle registered successfully on the surface.";
        public const string RoverCommandsCompletedSuccessfullyMessage = "Commands worked successfully.\nThe position and direction of the rover vehicle on the surface: ";
        public const string RoverCommandsCompletedUnsuccessfullyMessage= "Commands could not be run. The rover vehicle has returned to its original position.\nThe position and direction of the rover vehicle on the surface: ";
        public const string SurfaceAreaNotDefinedMessage = "The rover cannot be positioned because the surface area is not created.";
        public const string InvalidSurfaceWidthParametersMessage = "Width parameters must be greater than 0.";
        public const string AnotherRoverSpawnMessage = "\n\nAnother rover will be spawned.";
    }
}
