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
                //Application starting with start message. 
                Console.WriteLine(Messages.START_MESSAGE);

                //Asking surface size to user
                SurfaceProperty surfaceProperty = InputManagerService.GetTheSurfaceSize();

                //Generating singleton surface service with the parameters entered by the user. 
                ServiceProvider serviceProvider = InjectionServiceProvider.Builder();
                ISurface surface = serviceProvider.GetService<ISurface>();

                surface.SurfaceBuilder(surfaceProperty.width, surfaceProperty.height);

                //rover 1
                //For definition the first rover vehicle, asking location and direction in the surface.
                VehiclePositionProperty vehiclePositionPropertyForFirstRover = InputManagerService.GetRoverDefinition();

                //rover building
                RoverVehicle firstRover = new RoverVehicle(vehiclePositionPropertyForFirstRover.locationOnTheXAxis,
                    vehiclePositionPropertyForFirstRover.locationOnTheYAxis,
                    vehiclePositionPropertyForFirstRover.vehicleDirectionState, surface);

                //To move the rover, the user is prompted to enter commands
                List<char> commandForFirstRover = InputManagerService.GetRoverCommands();

                //Commands running
                CommandResult commandResultForFirstRover = firstRover.RunCommands(firstRover.roverId, firstRover.vehiclePositionProperty, commandForFirstRover);

                if (commandResultForFirstRover.isCommandFinishedSuccessfully)
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResultForFirstRover.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResultForFirstRover.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResultForFirstRover.vehicleNewPositionProperty.vehicleDirectionState));
                }
                else
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResultForFirstRover.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResultForFirstRover.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResultForFirstRover.vehicleNewPositionProperty.vehicleDirectionState));
                }

                /////////////////////


                //rover 2
                //For definition the second rover vehicle, asking location and direction in the surface.
                VehiclePositionProperty vehiclePositionPropertyForSecondRover = InputManagerService.GetRoverDefinition();

                //rover building
                RoverVehicle secondRover = new RoverVehicle(vehiclePositionPropertyForSecondRover.locationOnTheXAxis,
                    vehiclePositionPropertyForSecondRover.locationOnTheYAxis,
                    vehiclePositionPropertyForSecondRover.vehicleDirectionState, surface);

                //To move the rover, the user is prompted to enter commands
                List<char> commandsForSecondRover = InputManagerService.GetRoverCommands();

                //Commands running
                CommandResult commandResultForSecondRover = secondRover.RunCommands(secondRover.roverId, secondRover.vehiclePositionProperty, commandsForSecondRover);

                if (commandResultForSecondRover.isCommandFinishedSuccessfully)
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResultForSecondRover.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResultForSecondRover.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResultForSecondRover.vehicleNewPositionProperty.vehicleDirectionState));
                }
                else
                {
                    Console.WriteLine(Messages.ROVER_COMMANDS_COMPLETED_SUCCESSFULLY_MESSAGE + commandResultForSecondRover.vehicleNewPositionProperty.locationOnTheXAxis + " " + commandResultForSecondRover.vehicleNewPositionProperty.locationOnTheYAxis + " " + DirectionService.GetDirectionName(commandResultForSecondRover.vehicleNewPositionProperty.vehicleDirectionState));
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
