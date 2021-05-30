using MarsRoverControl.Consts;
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
                // Program starting with start message. 
                Console.WriteLine(Messages.START_MESSAGE);

                //Asking surface size to user
                SurfaceProperty surfaceProperty = InputManagerService.GetTheSurfaceSize();

                //Generating singleton surface service with the parameters entered by the user. 
                ServiceProvider serviceProvider = InjectionServiceProvider.Builder();
                ISurface surface = serviceProvider.GetService<ISurface>();

                surface.SurfaceBuilder(surfaceProperty.width, surfaceProperty.height);

                //rover 1
                //For definition the first rover vehicle, asking location and direction in the surface.
                VehiclePositionProperty vehiclePositionProperty = InputManagerService.GetRoverDefinition();

                //rover building
                RoverVehicle firstRover = new RoverVehicle(vehiclePositionProperty.locationOnTheXAxis,
                    vehiclePositionProperty.locationOnTheYAxis,
                    vehiclePositionProperty.vehicleDirectionState, surface);

                //To move the rover, the user is prompted to enter commands
                List<char> commands = InputManagerService.GetRoverCommands();

                //Commands running
                CommandResult commandResult = firstRover.RunCommands(firstRover.roverId, firstRover.vehiclePositionProperty, commands);

                if (commandResult.isCommandFinishedSuccessfully)
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResult.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResult.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResult.vehicleNewPositionProperty.vehicleDirectionState));
                }
                else
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResult.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResult.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResult.vehicleNewPositionProperty.vehicleDirectionState));
                }

                /////////////////////


                //rover 2
                //For definition the second rover vehicle, asking location and direction in the surface.
                VehiclePositionProperty vehiclePositionProperty2 = InputManagerService.GetRoverDefinition();

                //rover building
                RoverVehicle secondRover = new RoverVehicle(vehiclePositionProperty2.locationOnTheXAxis,
                    vehiclePositionProperty2.locationOnTheYAxis,
                    vehiclePositionProperty2.vehicleDirectionState, surface);

                //To move the rover, the user is prompted to enter commands
                List<char> commands2 = InputManagerService.GetRoverCommands();

                //Commands running
                CommandResult commandResult2 = secondRover.RunCommands(secondRover.roverId, secondRover.vehiclePositionProperty, commands2);

                if (commandResult2.isCommandFinishedSuccessfully)
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResult2.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResult2.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResult2.vehicleNewPositionProperty.vehicleDirectionState));
                }
                else
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResult2.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResult2.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResult2.vehicleNewPositionProperty.vehicleDirectionState));
                }
                Console.ReadLine();

                //////////////

            }
            catch (Exception ex)
            {
                Console.WriteLine("Status Code : InternalError. An error has been occured: Exception : " + ex.ToString());
            }
        }


    }


}
