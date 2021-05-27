using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface ISurface
    {
        public List<SurfacePoint> SurfacePointList { get; set; }
        public int SurfaceCode { get; set; }

        public void SurfaceBuilder(int _pointCountOnXAxis, int _pointCountOnYAxis);
        //public void GenerateRover(int _locationOnTheXAxis, int _locationOnTheYAxis, int _vehicleDirectionState);
        public SurfacePoint GetSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis);

    }
}
