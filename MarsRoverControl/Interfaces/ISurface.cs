using MarsRoverControl.Models;
using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface ISurface
    {
        List<SurfacePoint> surfacePointList { get; set; }
        void SurfaceBuilder(int _pointCountOnXAxis, int _pointCountOnYAxis);
        SurfacePoint GetSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis);
        bool VehicleMovePermissionControlForSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis, Guid _roverId);
        SurfacePoint GetRoverLocation(Guid roverId);
        bool VehicleRegistrationToSurface(RoverVehicle roverVehicle);
        RoverVehicle GetRoverWithId(Guid roverId);
    }
}
