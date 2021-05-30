using MarsRoverControl.Consts;
using MarsRoverControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverControl.Service
{
    public class Surface : ISurface
    {
        public List<SurfacePoint> surfacePointList { get; set; }
        public List<RoverVehicle> roverVehicleList { get; set; }

        public void SurfaceBuilder(int pointCountOnXAxis, int pointCountOnYAxis)
        {
            List<SurfacePoint> surfacePoints = new List<SurfacePoint>();

            //Generating surface points. Like x y coordinate system
            for (var y = 0; y <= pointCountOnYAxis; y++)
            {
                for (var x = 0; x <= pointCountOnXAxis; x++)
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
        public SurfacePoint GetSurfacePoint(int locationOnTheXAxis, int locationOnTheYAxis)
        {
            if (this.surfacePointList == null)
            {
                return null;
            }

            return this.surfacePointList.Where(x => x.locationOnTheXAxis == locationOnTheXAxis && x.locationOnTheYAxis == locationOnTheYAxis).FirstOrDefault();
        }

        /// <summary>
        /// It checks the suitability of the given point to move.
        /// </summary>
        /// <param name="_locationOnTheXAxis"></param>
        /// <param name="_locationOnTheYAxis"></param>
        /// <returns>bool result</returns>
        public bool VehicleMovePermissionControlForSurfacePoint(int locationOnTheXAxis, int locationOnTheYAxis, Guid roverId)
        {
            if (GetSurfacePoint(locationOnTheXAxis, locationOnTheYAxis) == null)
            {
                Console.WriteLine(Messages.OutsideSurfaceAreaMessage + " x : " + locationOnTheXAxis + " y : " + locationOnTheYAxis);
                return false;
            }

            bool anyRoverExistInThisPosition = false;

            if (roverVehicleList != null)
            {
                RoverVehicle activeRoverListDifferentCurrentRover = roverVehicleList.Where(x => x.roverId != roverId && x.vehiclePositionProperty.locationOnTheXAxis == locationOnTheXAxis
                && x.vehiclePositionProperty.locationOnTheYAxis == locationOnTheYAxis).FirstOrDefault();

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

        /// <summary>
        /// This mothod using for defining vehicle on the surface. In this method checks the vehicle has been registered before.
        /// </summary>
        /// <param name="roverVehicle"></param>
        /// <returns>bool result</returns>
        public bool VehicleRegistrationToSurface(RoverVehicle roverVehicle)
        {
            bool isOperationCompletedSuccessfully = false;
            if (roverVehicleList == null)
            {
                roverVehicleList = new List<RoverVehicle>();
            }

            if (GetRoverWithId(roverVehicle.roverId) == null)
            {
                //rover can be registered.
                roverVehicleList.Add(roverVehicle);
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
            if (roverVehicleList == null)
            {
                return null;
            }
            RoverVehicle roverVehicle = roverVehicleList.Where(x => x.roverId == roverId).FirstOrDefault();
            return roverVehicle;
        }
    }
}
