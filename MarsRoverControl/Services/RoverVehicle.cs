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
        public VehiclePositionProperty Position { get; set; }
        public ISurface Surface { get; set; }
        public Guid RoverId { get; set; }
        public bool IsActive { get; set; }

        public RoverVehicle(int locationOnTheXAxis, int locationOnTheYAxis, Direction vehicleDirectionState, ISurface definedSurface)
        {
            IsActive = true;

            if (definedSurface == null)
            {
                Console.WriteLine(Messages.SurfaceAreaNotDefinedMessage);
                IsActive = false;
            }

            RoverId = Guid.NewGuid();

            if (!definedSurface.VehicleMovePermissionControlForLocation(locationOnTheXAxis, locationOnTheYAxis, RoverId))
            {
                //Message forward to user in VehicleMovePermissionControlForSurfacePoint method.
                IsActive = false;
            }

            if (IsActive)
            {
                Position = new VehiclePositionProperty();
                Position.Location = new Location(locationOnTheXAxis, locationOnTheYAxis);
                Position.VehicleDirection = vehicleDirectionState;
                Surface = definedSurface;
                Surface.VehicleRegistrationToSurface(this);
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
                    if (commandResult.Success)
                    {
                        vehiclePositionProperty = commandResult.VehiclePosition;
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
                    if (commandResult.Success)
                    {
                        vehiclePositionProperty = commandResult.VehiclePosition;
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
                    if (commandResult.Success)
                    {
                        vehiclePositionProperty = commandResult.VehiclePosition;
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
                Success = isSimulationFinishedSuccesfully,
                VehiclePosition = isSimulationFinishedSuccesfully ? vehiclePositionProperty : storeObject
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

            if (commandResult.Success)
            {
                //New coordinates setting to the rover vehicle.
                RoverVehicle roverVehicle = Surface.GetRoverWithId(roverId);
                roverVehicle.Position = commandResult.VehiclePosition;
            }
            return commandResult;
        }

        /// <summary>
        /// This method sets the direction and returns the command result object as response. There is no need for control.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult TurnLeft(VehiclePositionProperty position)
        {
            if (position.VehicleDirection > 0)
            {

                position.VehicleDirection--;
            }
            else
            {
                position.VehicleDirection = EnumerationHelper<Direction>.GetEnumObjectByValue(EnumerationHelper<Direction>.GetEnumItemsCount() - 1);
            }

            return new CommandResult { Success = true, VehiclePosition = position };
        }

        /// <summary>
        /// This method sets the direction and returns the command result object as response. There is no need for control.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult TurnRight(VehiclePositionProperty position)
        {
            position.VehicleDirection = EnumerationHelper<Direction>.GetEnumObjectByValue(((int)position.VehicleDirection + 1) % EnumerationHelper<Direction>.GetEnumItemsCount());
            return new CommandResult { Success = true, VehiclePosition = position };
        }

        /// <summary>
        /// This method developed to move the vehicle forward. This method must have some validation.
        /// </summary>
        /// <param name="roverId"></param>
        /// <param name="position"></param>
        /// <returns>CommandResult object</returns>
        public CommandResult MoveForward(Guid roverId, VehiclePositionProperty position)
        {
            bool isCommandFinishedSuccessfully = false;
            if (position != null)
            {
                //VehicleMovePermissionControlForSurfacePoint method should be run. Beceause it checks the suitability of the given point to move. For example surface size, any different vehicle..
                switch ((int)position.VehicleDirection)
                {
                    case 0:
                        if (Surface.VehicleMovePermissionControlForLocation(position.Location.X, position.Location.Y + 1, roverId))
                        {
                            position.Location.Y++;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 1:
                        if (Surface.VehicleMovePermissionControlForLocation(position.Location.X + 1, position.Location.Y, roverId))
                        {
                            position.Location.X++;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 2:
                        if (Surface.VehicleMovePermissionControlForLocation(position.Location.X, position.Location.Y - 1, roverId))
                        {
                            position.Location.Y--;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    case 3:
                        if (Surface.VehicleMovePermissionControlForLocation(position.Location.X - 1, position.Location.Y, roverId))
                        {
                            position.Location.X--;
                            isCommandFinishedSuccessfully = true;
                        }
                        break;
                    default:
                        Console.WriteLine(Messages.UndefinedDirectionMessage);
                        break;
                }
            }

            return new CommandResult { Success = isCommandFinishedSuccessfully, VehiclePosition = position };
        }

        public string GetRoverPositionOnSurface()
        {
            return Position.Location.X + " " + Position.Location.Y;
        }

    }
}
