using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Models
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public VehiclePositionProperty VehiclePosition { get; set; }
    }
}
