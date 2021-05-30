using MarsRoverControl.Models;
using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface ISurface
    {
        void SurfaceBuilder(int pointCountOnXAxis, int pointCountOnYAxis);
        Position GetSurfacePoint(int locationOnTheXAxis, int locationOnTheYAxis);
        bool VehicleMovePermissionControlForSurfacePoint(int locationOnTheXAxis, int locationOnTheYAxis, Guid roverId);
        Position GetRoverLocation(Guid roverId);
        bool VehicleRegistrationToSurface(RoverVehicle roverVehicle);
        RoverVehicle GetRoverWithId(Guid roverId);
    }
}
