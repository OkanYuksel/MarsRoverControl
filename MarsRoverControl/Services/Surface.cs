using MarsRoverControl.Consts;
using MarsRoverControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverControl.Service
{
    public class Surface : ISurface
    {
        public List<Location> SurfacePointList { get; set; }
        public List<RoverVehicle> RoverVehicleList { get; set; }

        public void SurfaceBuilder(int pointCountOnXAxis, int pointCountOnYAxis)
        {
            List<Location> surfacePoints = new List<Location>();

            //Generating surface points. Like x y coordinate system
            for (var y = 0; y <= pointCountOnYAxis; y++)
            {
                for (var x = 0; x <= pointCountOnXAxis; x++)
                {
                    surfacePoints.Add(new Location(x, y));
                }
            }

            SurfacePointList = surfacePoints;
        }

        /// <summary>
        /// This method finds the surface point whose coordinates are given.
        /// </summary>
        /// <param name="locationOnTheXAxis"></param>
        /// <param name="locationOnTheYAxis"></param>
        /// <returns>Location object</returns>
        public Location GetLocation(int locationOnTheXAxis, int locationOnTheYAxis)
        {
            if (SurfacePointList == null)
            {
                return null;
            }

            return SurfacePointList.Where(sp => sp.X == locationOnTheXAxis && sp.Y == locationOnTheYAxis).FirstOrDefault();
        }

        /// <summary>
        /// It checks the suitability of the given point to move.
        /// </summary>
        /// <param name="locationOnTheXAxis"></param>
        /// <param name="locationOnTheYAxis"></param>
        /// /// <param name="roverId"></param>
        /// <returns>bool result</returns>
        public bool VehicleMovePermissionControlForLocation(int locationOnTheXAxis, int locationOnTheYAxis, Guid roverId)
        {
            if (GetLocation(locationOnTheXAxis, locationOnTheYAxis) == null)
            {
                Console.WriteLine(Messages.OutsideSurfaceAreaMessage + " x : " + locationOnTheXAxis + " y : " + locationOnTheYAxis);
                return false;
            }

            bool anyRoverExistInThisPosition = false;

            if (RoverVehicleList != null)
            {
                RoverVehicle activeRoverListDifferentCurrentRover = RoverVehicleList.Where(x => x.RoverId != roverId && x.Position.Location.X == locationOnTheXAxis
                && x.Position.Location.Y == locationOnTheYAxis).FirstOrDefault();

                anyRoverExistInThisPosition = (activeRoverListDifferentCurrentRover != null);
                if (anyRoverExistInThisPosition)
                {
                    Console.WriteLine(Messages.VehicleCheckOnSurfacePointMessage + " x : " + locationOnTheXAxis + " y : " + locationOnTheYAxis);
                }
            }

            return !anyRoverExistInThisPosition;
        }

        /// <summary>
        /// Finds the rover's current location.
        /// </summary>
        /// <param name="roverId"></param>
        /// <returns>SurfacePoint object</returns>
        public Location GetRoverLocation(Guid roverId)
        {
            Location location = null;
            if (RoverVehicleList != null)
            {
                RoverVehicle roverVehicle = GetRoverWithId(roverId);
                if (roverVehicle != null)
                {
                    location = GetLocation(roverVehicle.Position.Location.X, roverVehicle.Position.Location.Y);
                }
            }
            return location;
        }

        /// <summary>
        /// This mothod using for defining vehicle on the surface. In this method checks the vehicle has been registered before.
        /// </summary>
        /// <param name="roverVehicle"></param>
        /// <returns>bool result</returns>
        public bool VehicleRegistrationToSurface(RoverVehicle roverVehicle)
        {
            bool isOperationCompletedSuccessfully = false;
            if (RoverVehicleList == null)
            {
                RoverVehicleList = new List<RoverVehicle>();
            }

            if (GetRoverWithId(roverVehicle.RoverId) == null)
            {
                //rover can be registered.
                RoverVehicleList.Add(roverVehicle);
                isOperationCompletedSuccessfully = true;
                Console.WriteLine(Messages.RoverSuccessfullyRegisteredMessage);
            }
            else
            {
                Console.WriteLine(Messages.RoverHasBeenRegisteredMessage);
            }

            return isOperationCompletedSuccessfully;
        }

        /// <summary>
        ///  Finds the vehicle with roverId in roverVehicleList.
        /// </summary>
        /// <param name="roverId"></param>
        /// <returns>RoverVehicle object</returns>
        public RoverVehicle GetRoverWithId(Guid roverId)
        {
            if (RoverVehicleList == null)
            {
                return null;
            }
            RoverVehicle roverVehicle = RoverVehicleList.Where(x => x.RoverId == roverId).FirstOrDefault();
            return roverVehicle;
        }
    }
}
