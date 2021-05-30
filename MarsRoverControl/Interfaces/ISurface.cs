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
        Location GetLocation(int locationOnTheXAxis, int locationOnTheYAxis);
        bool VehicleMovePermissionControlForLocation(int locationOnTheXAxis, int locationOnTheYAxis, Guid roverId);
        bool VehicleRegistrationToSurface(RoverVehicle roverVehicle);
        RoverVehicle GetRoverWithId(Guid roverId);
    }
}
