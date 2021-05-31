using MarsRoverControl.Consts;
using MarsRoverControl.Enums;
using MarsRoverControl.Interfaces;
using MarsRoverControl.Models;
using MarsRoverControl.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarsRoverControl
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Application starting with start message. 
                Console.WriteLine(Messages.StartMessage);

                //Asking surface size to user
                SurfaceSize surfaceSize = InputManagerService.GetTheSurfaceSize();

                //Generating singleton surface service with the parameters entered by the user. 
                ServiceProvider serviceProvider = InjectionServiceProvider.Builder();
                ISurface surface = serviceProvider.GetService<ISurface>();

                surface.SurfaceBuilder(surfaceSize.Width, surfaceSize.Height);

                for (int i = 0; i < Settings.HowManyRoverWillResearch; i++)
                {
                    if (i != 0)
                    {
                        Console.WriteLine(Messages.AnotherRoverSpawnMessage);
                    }

                    CreateRoverOnTheSurface(surface);
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Status Code : InternalError. An error has been occured: Exception : " + ex.ToString());
            }
        }

        /// <summary>
        /// This method builds new rover and moves rover
        /// </summary>
        /// <param name="surface"></param>
        private static void CreateRoverOnTheSurface(ISurface surface)
        {
            bool roverCreatedSuccessfully = false;
            do
            {
                //For definition the rover vehicle, asking location and direction in the surface.
                VehiclePosition vehiclePositionForRover = InputManagerService.GetRoverDefinition();

                //rover building
                RoverVehicle rover = new RoverVehicle(vehiclePositionForRover.Location.X,
                    vehiclePositionForRover.Location.Y,
                    vehiclePositionForRover.VehicleDirection, surface);

                if (rover.IsActive)
                {
                    //rover created successfully.
                    roverCreatedSuccessfully = true;
                    DoCommandOperationsForRover(rover);
                }
            } while (roverCreatedSuccessfully == false);
        }


        /// <summary>
        /// This method is used to move the rover. And writes outputs the new position of the rover.
        /// </summary>
        /// <param name="rover"></param>
        private static void DoCommandOperationsForRover(RoverVehicle rover)
        {
            bool roverMovedSuccessfully = false;
            do
            {
                //To move the rover, the user is prompted to enter commands
                List<char> commandListForRover = InputManagerService.GetRoverCommands();

                //Commands running
                CommandResult commandResultForRover = rover.RunCommands(rover.RoverId, rover.Position, commandListForRover);

                if (commandResultForRover.Success)
                {
                    roverMovedSuccessfully = true;
                    Console.WriteLine(
                        Messages.RoverCommandsCompletedSuccessfullyMessage +
                        commandResultForRover.VehiclePosition.Location.X + " " +
                        commandResultForRover.VehiclePosition.Location.Y + " " +
                        EnumerationHelper<Direction>.GetEnumItemName((int)commandResultForRover.VehiclePosition.VehicleDirection));
                }
                else
                {
                    Console.WriteLine(Messages.RoverCommandsCompletedUnsuccessfullyMessage + 
                        commandResultForRover.VehiclePosition.Location.X + " " + 
                        commandResultForRover.VehiclePosition.Location.Y + " " + 
                        EnumerationHelper<Direction>.GetEnumItemName((int)commandResultForRover.VehiclePosition.VehicleDirection));
                }
            } while (roverMovedSuccessfully == false);
        }
    }


}
