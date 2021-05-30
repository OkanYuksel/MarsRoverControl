using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Models
{
    public class CommandResult
    {
        public bool IsCommandFinishedSuccessfully { get; set; }
        public VehiclePositionProperty VehicleNewPositionProperty { get; set; }
    }
}
