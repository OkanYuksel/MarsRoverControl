using MarsRoverControl.Models;
using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface IRoverVehicle
    {
        CommandResult TurnLeft(VehiclePositionProperty vehiclePositionProperty);
        CommandResult TurnRight(VehiclePositionProperty vehiclePositionProperty);
        CommandResult MoveForward(Guid roverId, VehiclePositionProperty vehiclePositionProperty);
        string GetRoverPositionAndDirection();
        string GetRoverPositionOnSurface();
        CommandResult SimulationForTheCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList);
        CommandResult RunCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList);
    }
}
