using MarsRoverControl.Consts;
using MarsRoverControl.Enums;
using MarsRoverControl.Interfaces;
using MarsRoverControl.Models;
using System;
using System.Collections.Generic;
using static MarsRoverControl.Service.InputManagerService;

namespace MarsRoverControl.Service
{
    public class RoverVehicle : IRoverVehicle
    {
        public VehiclePositionProperty vehiclePositionProperty { get; set; }
        public ISurface surface { get; set; }
        public Guid roverId { get; set; }
        public bool isActive { get; set; }

        public RoverVehicle(int locationOnTheXAxis, int locationOnTheYAxis, int vehicleDirectionState, ISurface definedSurface)
        {
            isActive = true;

            if (definedSurface == null)
            {
                Console.WriteLine(Messages.SurfaceAreaNotDefinedMessage);
                isActive = false;
            }

            roverId = Guid.NewGuid();

            if (!definedSurface.VehicleMovePermissionControlForSurfacePoint(locationOnTheXAxis, locationOnTheYAxis, roverId))
            {
                //Message forward to user in VehicleMovePermissionControlForSurfacePoint method.
                isActive = false;
            }

            if (isActive)
            {
                vehiclePositionProperty = new VehiclePositionProperty();
                vehiclePositionProperty.PositionOnSurface = new Position(locationOnTheXAxis, locationOnTheYAxis);
                vehiclePositionProperty.VehicleDirectionState = vehicleDirectionState;
                surface = definedSurface;
                surface.VehicleRegistrationToSurface(this);
            }
        }

        /// <summary>
        /// It simulates the commands that comes as a parameter and checks for the problem.
        /// </summary>
        /// <param name="roverId"></param>
        /// <param name="vehiclePositionProperty"></param>
        /// <param name="commandList"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult SimulationForTheCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList)
        {
            VehiclePositionProperty storeObject = vehiclePositionProperty.Clone();

            bool isSimulationFinishedSuccesfully = true;
            foreach (var command in commandList)
            {
                if (command.ToString() == EnumerationHelper<Command>.GetEnumItemName((int)Command.L))
                {
                    CommandResult commandResult = TurnLeft(vehiclePositionProperty);
                    if (commandResult.IsCommandFinishedSuccessfully)
                    {
                        vehiclePositionProperty = commandResult.VehicleNewPositionProperty;
                    }
                    else
                    {
                        isSimulationFinishedSuccesfully = false;
                        break;
                    }
                }
                else if (command.ToString() == EnumerationHelper<Command>.GetEnumItemName((int)Command.R))
                {
                    CommandResult commandResult = TurnRight(vehiclePositionProperty);
                    if (commandResult.IsCommandFinishedSuccessfully)
                    {
                        vehiclePositionProperty = commandResult.VehicleNewPositionProperty;
                    }
                    else
                    {
                        isSimulationFinishedSuccesfully = false;
                        break;
                    }
                }
                else if (command.ToString() == EnumerationHelper<Command>.GetEnumItemName((int)Command.M))
                {
                    CommandResult commandResult = MoveForward(roverId, vehiclePositionProperty);
                    if (commandResult.IsCommandFinishedSuccessfully)
                    {
                        vehiclePositionProperty = commandResult.VehicleNewPositionProperty;
                    }
                    else
                    {
                        isSimulationFinishedSuccesfully = false;
                        break;
                    }
                }
            }

            return new CommandResult
            {
                IsCommandFinishedSuccessfully = isSimulationFinishedSuccesfully,
                VehicleNewPositionProperty = isSimulationFinishedSuccesfully ? vehiclePositionProperty : storeObject
            };
        }

        /// <summary>
        /// If there is no problem with the simulated commands, it moves the vehicle to the specified coordinate.
        /// </summary>
        /// <param name="roverId"></param>
        /// <param name="vehiclePositionProperty"></param>
        /// <param name="commandList"></param>
        /// <returns></returns>
        public CommandResult RunCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList)
        {
            CommandResult commandResult = SimulationForTheCommands(roverId, vehiclePositionProperty, commandList);

            if (commandResult.IsCommandFinishedSuccessfully)
            {
                //New coordinates setting to the rover vehicle.
                RoverVehicle roverVehicle = surface.GetRoverWithId(roverId);
                roverVehicle.vehiclePositionProperty = commandResult.VehicleNewPositionProperty;
            }
            return commandResult;
        }

        /// <summary>
        /// This method sets the direction and returns the command result object as response. There is no need for control.
        /// </summary>
        /// <param name="vehiclePositionProperty"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult TurnLeft(VehiclePositionProperty vehiclePositionProperty)
        {
            if (vehiclePositionProperty.VehicleDirectionState > 0)
            {

                vehiclePositionProperty.VehicleDirectionState -= 1;
            }
            else
            {
                vehiclePositionProperty.VehicleDirectionState = EnumerationHelper<Direction>.GetEnumItemsCount() - 1;
            }

            return new CommandResult { IsCommandFinishedSuccessfully = true, VehicleNewPositionProperty = vehiclePositionProperty };
        }

        /// <summary>
        /// This method sets the direction and returns the command result object as response. There is no need for control.
        /// </summary>
        /// <param name="vehiclePositionProperty"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult TurnRight(VehiclePositionProperty vehiclePositionProperty)
        {
            vehiclePositionProperty.VehicleDirectionState = (vehiclePositionProperty.VehicleDirectionState + 1) % EnumerationHelper<Direction>.GetEnumItemsCount();
            return new CommandResult { IsCommandFinishedSuccessfully = true, VehicleNewPositionProperty = vehiclePositionProperty };
        }

        /// <summary>
        /// This method developed to move the vehicle forward. This method must have some validation.
        /// </summary>
        /// <param name="roverId"></param>
        /// <param name="vehiclePositionProperty"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult MoveForward(Guid roverId, VehiclePositionProperty vehiclePositionProperty)
        {
            bool isCommandFinishedSuccessfully = false;
            if (vehiclePositionProperty != null)
            {
                //VehicleMovePermissionControlForSurfacePoint method should be run. Beceause it checks the suitability of the given point to move. For example surface size, any different vehicle..
                switch (vehiclePositionProperty.VehicleDirectionState)
                {
                    case 0:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.PositionOnSurface.X, vehiclePositionProperty.PositionOnSurface.Y + 1, roverId))
                        {
                            vehiclePositionProperty.PositionOnSurface.Y++;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 1:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.PositionOnSurface.X + 1, vehiclePositionProperty.PositionOnSurface.Y, roverId))
                        {
                            vehiclePositionProperty.PositionOnSurface.X++;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 2:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.PositionOnSurface.X, vehiclePositionProperty.PositionOnSurface.Y - 1, roverId))
                        {
                            vehiclePositionProperty.PositionOnSurface.Y--;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 3:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.PositionOnSurface.X - 1, vehiclePositionProperty.PositionOnSurface.Y, roverId))
                        {
                            vehiclePositionProperty.PositionOnSurface.X--;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    default:
                        Console.WriteLine(Messages.UndefinedDirectionMessage);
                        break;
                }
            }

            return new CommandResult { IsCommandFinishedSuccessfully = isCommandFinishedSuccessfully, VehicleNewPositionProperty = vehiclePositionProperty };
        }

        public string GetRoverPositionOnSurface()
        {
            return vehiclePositionProperty.PositionOnSurface.X + " " + vehiclePositionProperty.PositionOnSurface.Y;
        }

    }
}
