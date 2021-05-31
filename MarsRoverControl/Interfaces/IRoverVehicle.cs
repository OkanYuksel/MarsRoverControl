using MarsRoverControl.Models;
using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface IRoverVehicle
    {
        CommandResult TurnLeft(VehiclePosition vehiclePosition);
        CommandResult TurnRight(VehiclePosition vehiclePosition);
        CommandResult MoveForward(Guid roverId, VehiclePosition vehiclePosition);
        string GetRoverPositionOnSurface();
        CommandResult SimulationForTheCommands(Guid roverId, VehiclePosition vehiclePosition, List<char> commandList);
        CommandResult RunCommands(Guid roverId, VehiclePosition vehiclePosition, List<char> commandList);
    }
}
