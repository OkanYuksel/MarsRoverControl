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

        public RoverVehicle(int _locationOnTheXAxis, int _locationOnTheYAxis, int _vehicleDirectionState, ISurface _surface)
        {
            isActive = true;

            if (_surface == null)
            {
                Console.WriteLine(Messages.SURFACE_AREA_NOT_DEFINED_MESSAGE);
                isActive = false;
            }

            roverId = Guid.NewGuid();

            if (!_surface.VehicleMovePermissionControlForSurfacePoint(_locationOnTheXAxis, _locationOnTheYAxis, roverId))
            {
                //Message forward to user in VehicleMovePermissionControlForSurfacePoint method.
                isActive = false;
            }

            if (isActive)
            {
                vehiclePositionProperty = new VehiclePositionProperty();
                vehiclePositionProperty.locationOnTheXAxis = _locationOnTheXAxis;
                vehiclePositionProperty.locationOnTheYAxis = _locationOnTheYAxis;
                vehiclePositionProperty.vehicleDirectionState = _vehicleDirectionState;
                surface = _surface;
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
                if (command.ToString() == EnumerationHelper<Commands>.GetEnumItemName((int)Commands.L))
                {
                    CommandResult commandResult = TurnLeft(vehiclePositionProperty);
                    if (commandResult.isCommandFinishedSuccessfully)
                    {
                        vehiclePositionProperty = commandResult.vehicleNewPositionProperty;
                    }
                    else
                    {
                        isSimulationFinishedSuccesfully = false;
                        break;
                    }
                }
                else if (command.ToString() == EnumerationHelper<Commands>.GetEnumItemName((int)Commands.R))
                {
                    CommandResult commandResult = TurnRight(vehiclePositionProperty);
                    if (commandResult.isCommandFinishedSuccessfully)
                    {
                        vehiclePositionProperty = commandResult.vehicleNewPositionProperty;
                    }
                    else
                    {
                        isSimulationFinishedSuccesfully = false;
                        break;
                    }
                }
                else if (command.ToString() == EnumerationHelper<Commands>.GetEnumItemName((int)Commands.M))
                {
                    CommandResult commandResult = MoveForward(roverId, vehiclePositionProperty);
                    if (commandResult.isCommandFinishedSuccessfully)
                    {
                        vehiclePositionProperty = commandResult.vehicleNewPositionProperty;
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
                isCommandFinishedSuccessfully = isSimulationFinishedSuccesfully,
                vehicleNewPositionProperty = isSimulationFinishedSuccesfully ? vehiclePositionProperty : storeObject
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

            if (commandResult.isCommandFinishedSuccessfully)
            {
                //New coordinates setting to the rover vehicle.
                RoverVehicle roverVehicle = surface.GetRoverWithId(roverId);
                roverVehicle.vehiclePositionProperty = commandResult.vehicleNewPositionProperty;
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
            if (vehiclePositionProperty.vehicleDirectionState > 0)
            {

                vehiclePositionProperty.vehicleDirectionState -= 1;
            }
            else
            {
                vehiclePositionProperty.vehicleDirectionState = EnumerationHelper<RoverDirections>.GetEnumItemsCount() - 1;
            }

            return new CommandResult { isCommandFinishedSuccessfully = true, vehicleNewPositionProperty = vehiclePositionProperty };
        }

        /// <summary>
        /// This method sets the direction and returns the command result object as response. There is no need for control.
        /// </summary>
        /// <param name="vehiclePositionProperty"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult TurnRight(VehiclePositionProperty vehiclePositionProperty)
        {
            vehiclePositionProperty.vehicleDirectionState = (vehiclePositionProperty.vehicleDirectionState + 1) % EnumerationHelper<RoverDirections>.GetEnumItemsCount();
            return new CommandResult { isCommandFinishedSuccessfully = true, vehicleNewPositionProperty = vehiclePositionProperty };
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
                switch (vehiclePositionProperty.vehicleDirectionState)
                {
                    case 0:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis + 1, roverId))
                        {
                            vehiclePositionProperty.locationOnTheYAxis += 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 1:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis + 1, vehiclePositionProperty.locationOnTheYAxis, roverId))
                        {
                            vehiclePositionProperty.locationOnTheXAxis += 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 2:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis - 1, roverId))
                        {
                            vehiclePositionProperty.locationOnTheYAxis -= 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 3:
                        if (surface.VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis - 1, vehiclePositionProperty.locationOnTheYAxis, roverId))
                        {
                            vehiclePositionProperty.locationOnTheXAxis -= 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    default:
                        Console.WriteLine(Messages.UNDEFINED_DIRECTION_MESSAGE);
                        break;
                }
            }

            return new CommandResult { isCommandFinishedSuccessfully = isCommandFinishedSuccessfully, vehicleNewPositionProperty = vehiclePositionProperty };
        }

        public string GetRoverPositionAndDirection()
        {
            return GetRoverPositionOnSurface() + " " + Enum.GetName(typeof(RoverDirections), vehiclePositionProperty.vehicleDirectionState);
        }

        public string GetRoverPositionOnSurface()
        {
            return vehiclePositionProperty.locationOnTheXAxis + " " + vehiclePositionProperty.locationOnTheYAxis;
        }

    }
}
