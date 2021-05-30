using MarsRoverControl.Interfaces;
using MarsRoverControl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverControl.Service
{
    public class Surface : ISurface
    {
        public List<SurfacePoint> surfacePointList { get; set; }
        public List<RoverVehicle> roverVehicleList { get; set; }

        public void SurfaceBuilder(int _pointCountOnXAxis, int _pointCountOnYAxis)
        {
            List<SurfacePoint> surfacePoints = new List<SurfacePoint>();

            for (var y = 0; y <= _pointCountOnYAxis; y += 1)
            {
                for (var x = 0; x <= _pointCountOnXAxis; x += 1)
                {
                    surfacePoints.Add(new SurfacePoint(x, y));
                }
            }

            surfacePointList = surfacePoints;
        }

        /// <summary>
        /// This method finds the surface point whose coordinates are given.
        /// </summary>
        /// <param name="_locationOnTheXAxis"></param>
        /// <param name="_locationOnTheYAxis"></param>
        /// <returns>SurfacePoint object</returns>
        public SurfacePoint GetSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis)
        {
            if (this.surfacePointList == null)
            {
                return null;
            }

            return this.surfacePointList.Where(x => x.locationOnTheXAxis == _locationOnTheXAxis && x.locationOnTheYAxis == _locationOnTheYAxis).FirstOrDefault();
        }

        /// <summary>
        /// It checks the suitability of the given point to move.
        /// </summary>
        /// <param name="_locationOnTheXAxis"></param>
        /// <param name="_locationOnTheYAxis"></param>
        /// <returns>bool result</returns>
        public bool VehicleMovePermissionControlForSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis, Guid _roverId)
        {
            if (GetSurfacePoint(_locationOnTheXAxis, _locationOnTheYAxis) == null)
            {
                Console.WriteLine("Gitmek istediğin koordinat yüzey alanının dışında. x : " + _locationOnTheXAxis + " y : " + _locationOnTheYAxis);
                return false;
            }

            bool anyRoverExistInThisPosition = false;

            if (roverVehicleList != null)
            {
                RoverVehicle activeRoverListDifferentCurrentRover = roverVehicleList.Where(x => x.roverId != _roverId && x.vehiclePositionProperty.locationOnTheXAxis == _locationOnTheXAxis
                && x.vehiclePositionProperty.locationOnTheYAxis == _locationOnTheYAxis).FirstOrDefault();

                anyRoverExistInThisPosition = (activeRoverListDifferentCurrentRover != null);
            }

            return !anyRoverExistInThisPosition;
        }

        public CommandResult SimulationForTheCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList)
        {
            VehiclePositionProperty storeObject = Clone(vehiclePositionProperty);

            bool isSimulationFinishedSuccesfully = true;
            foreach (var command in commandList)
            {
                if (command.ToString() == "L")
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
                else if (command.ToString() == "R")
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
                else if (command.ToString() == "M")
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

        public CommandResult RunCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList)
        {
            CommandResult commandResult = SimulationForTheCommands(roverId, vehiclePositionProperty, commandList);

            if (commandResult.isCommandFinishedSuccessfully)
            {
                RoverVehicle roverVehicle = GetRoverWithId(roverId);
                roverVehicle.vehiclePositionProperty = commandResult.vehicleNewPositionProperty;
            }
            return commandResult;
        }

        /// <summary>
        /// Moves the rover to new location.
        /// </summary>
        /// <param name="roverId"></param>
        /// <param name="vehiclePositionProperty"></param>
        /// <returns></returns>
        //public bool TransportVehicleToPoint(Guid roverId, VehiclePositionProperty vehiclePositionProperty)
        //{
        //    SurfacePoint currentSurfacePoint = GetRoverLocation(roverId);
        //    bool isOldLocationRemoved = false;
        //    bool newLocationBinded = false;
        //    foreach (var surfacePoint in surfacePointList)
        //    {
        //        if (surfacePoint.locationOnTheXAxis == currentSurfacePoint.locationOnTheXAxis && surfacePoint.locationOnTheYAxis == currentSurfacePoint.locationOnTheYAxis)
        //        {
        //            surfacePoint.rover = null;
        //            isOldLocationRemoved = true;
        //        }
        //        else if (surfacePoint.locationOnTheXAxis == vehiclePositionProperty.locationOnTheXAxis && surfacePoint.locationOnTheYAxis == vehiclePositionProperty.locationOnTheYAxis)
        //        {
        //            surfacePoint.rover = currentSurfacePoint.rover;
        //            newLocationBinded = true;
        //        }
        //    }

        //    if (isOldLocationRemoved && newLocationBinded)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Finds the rover's current location.
        /// </summary>
        /// <param name="roverId"></param>
        /// <returns>SurfacePoint object</returns>
        public SurfacePoint GetRoverLocation(Guid roverId)
        {
            SurfacePoint surfacePoint = null;
            if (roverVehicleList != null)
            {
                RoverVehicle roverVehicle = GetRoverWithId(roverId);
                if (roverVehicle != null)
                {
                    surfacePoint = GetSurfacePoint(roverVehicle.vehiclePositionProperty.locationOnTheXAxis, roverVehicle.vehiclePositionProperty.locationOnTheYAxis);
                }
            }
            return surfacePoint;
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public CommandResult TurnLeft(VehiclePositionProperty vehiclePositionProperty)
        {
            if (vehiclePositionProperty.vehicleDirectionState > 0)
            {

                vehiclePositionProperty.vehicleDirectionState -= 1;
            }
            else
            {
                vehiclePositionProperty.vehicleDirectionState = DirectionService.GetDirectionCount() - 1;
            }

            return new CommandResult { isCommandFinishedSuccessfully = true, vehicleNewPositionProperty = vehiclePositionProperty };
        }

        public CommandResult TurnRight(VehiclePositionProperty vehiclePositionProperty)
        {
            vehiclePositionProperty.vehicleDirectionState = (vehiclePositionProperty.vehicleDirectionState + 1) % DirectionService.GetDirectionCount();
            return new CommandResult { isCommandFinishedSuccessfully = true, vehicleNewPositionProperty = vehiclePositionProperty };
        }

        public CommandResult MoveForward(Guid roverId, VehiclePositionProperty vehiclePositionProperty)
        {
            bool isCommandFinishedSuccessfully = false;
            if (vehiclePositionProperty != null)
            {
                switch (vehiclePositionProperty.vehicleDirectionState)
                {
                    case 0:
                        if (VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis + 1, roverId))
                        {
                            vehiclePositionProperty.locationOnTheYAxis += 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        else
                        {
                            //TODO error
                        }

                        break;
                    case 1:
                        if (VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis + 1, vehiclePositionProperty.locationOnTheYAxis, roverId))
                        {
                            vehiclePositionProperty.locationOnTheXAxis += 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        else
                        {
                            //TODO error
                        }
                        break;
                    case 2:
                        if (VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis - 1, roverId))
                        {
                            vehiclePositionProperty.locationOnTheYAxis -= 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        else
                        {
                            //TODO error
                        }

                        break;
                    case 3:
                        if (VehicleMovePermissionControlForSurfacePoint(vehiclePositionProperty.locationOnTheXAxis - 1, vehiclePositionProperty.locationOnTheYAxis, roverId))
                        {
                            vehiclePositionProperty.locationOnTheXAxis -= 1;
                            isCommandFinishedSuccessfully = true;
                        }
                        else
                        {
                            //TODO error
                        }
                        break;
                    default:
                        Console.WriteLine("Geçersiz durum.");
                        break;
                }
            }

            return new CommandResult { isCommandFinishedSuccessfully = isCommandFinishedSuccessfully, vehicleNewPositionProperty = vehiclePositionProperty };
        }

        public void VehicleRegistrationToSurface(RoverVehicle roverVehicle)
        {
            if (roverVehicleList == null)
            {
                roverVehicleList = new List<RoverVehicle>();
            }
            roverVehicleList.Add(roverVehicle);
        }

        public RoverVehicle GetRoverWithId(Guid roverId)
        {
            RoverVehicle roverVehicle = roverVehicleList.Where(x => x.roverId == roverId).FirstOrDefault();
            return roverVehicle;
        }
    }
}
