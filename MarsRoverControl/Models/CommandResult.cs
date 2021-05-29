using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Models
{
    public class CommandResult
    {
        public bool isCommandFinishedSuccessfully { get; set; }
        public VehiclePositionProperty vehicleNewPositionProperty { get; set; }
    }
}
