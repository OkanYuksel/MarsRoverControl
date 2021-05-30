using MarsRoverControl.Models;
using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface ISurface
    {
        public List<SurfacePoint> surfacePointList { get; set; }
        public void SurfaceBuilder(int _pointCountOnXAxis, int _pointCountOnYAxis);
        public SurfacePoint GetSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis);
        public bool VehicleMovePermissionControlForSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis, Guid _roverId);
        //public CommandResult SimulationForTheCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList);
        //public CommandResult RunCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList);
        //public bool TransportVehicleToPoint(Guid roverId, VehiclePositionProperty vehiclePositionProperty);
        public SurfacePoint GetRoverLocation(Guid roverId);
        public void VehicleRegistrationToSurface(RoverVehicle roverVehicle);
        public RoverVehicle GetRoverWithId(Guid roverId);
    }
}
