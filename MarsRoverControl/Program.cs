using MarsRoverControl.Consts;
using MarsRoverControl.Enums;
using MarsRoverControl.Interfaces;
using MarsRoverControl.Models;
using MarsRoverControl.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
                SurfaceSizeProperty surfaceSizeProperty = InputManagerService.GetTheSurfaceSize();

                //Generating singleton surface service with the parameters entered by the user. 
                ServiceProvider serviceProvider = InjectionServiceProvider.Builder();
                ISurface surface = serviceProvider.GetService<ISurface>();

                surface.SurfaceBuilder(surfaceSizeProperty.width, surfaceSizeProperty.height);

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
                //For definition the first rover vehicle, asking location and direction in the surface.
                VehiclePositionProperty vehiclePositionPropertyForRover = InputManagerService.GetRoverDefinition();

                //rover building
                RoverVehicle rover = new RoverVehicle(vehiclePositionPropertyForRover.locationOnTheXAxis,
                    vehiclePositionPropertyForRover.locationOnTheYAxis,
                    vehiclePositionPropertyForRover.vehicleDirectionState, surface);

                if (rover.isActive)
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
            bool firstRoverMovedSuccessfully = false;
            do
            {
                //To move the rover, the user is prompted to enter commands
                List<char> commandListForRover = InputManagerService.GetRoverCommands();

                //Commands running
                CommandResult commandResultForRover = rover.RunCommands(rover.roverId, rover.vehiclePositionProperty, commandListForRover);

                if (commandResultForRover.isCommandFinishedSuccessfully)
                {
                    firstRoverMovedSuccessfully = true;
                    Console.WriteLine(Messages.RoverCommandsCompletedSuccessfullyMessage + commandResultForRover.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResultForRover.vehicleNewPositionProperty.locationOnTheYAxis + " " + EnumerationHelper<Direction>.GetEnumItemName(commandResultForRover.vehicleNewPositionProperty.vehicleDirectionState));
                }
                else
                {
                    Console.WriteLine(Messages.RoverCommandsCompletedUnsuccessfullyMessage + commandResultForRover.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResultForRover.vehicleNewPositionProperty.locationOnTheYAxis + " " + EnumerationHelper<Direction>.GetEnumItemName(commandResultForRover.vehicleNewPositionProperty.vehicleDirectionState));
                }
            } while (firstRoverMovedSuccessfully == false);
        }
    }


}
