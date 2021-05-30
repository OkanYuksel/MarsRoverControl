using MarsRoverControl.Models;
using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface IRoverVehicle
    {
        public CommandResult TurnLeft(VehiclePositionProperty vehiclePositionProperty);
        public CommandResult TurnRight(VehiclePositionProperty vehiclePositionProperty);
        public CommandResult MoveForward(Guid roverId, VehiclePositionProperty vehiclePositionProperty);
        public string GetRoverPositionAndDirection();
        public string GetRoverPositionOnSurface();
        public CommandResult SimulationForTheCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList);
        public CommandResult RunCommands(Guid roverId, VehiclePositionProperty vehiclePositionProperty, List<char> commandList);
    }
}
