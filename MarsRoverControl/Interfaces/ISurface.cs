using MarsRoverControl.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Interfaces
{
    public interface ISurface
    {
        public List<SurfacePoint> surfacePointList { get; set; }
        public int surfaceCode { get; set; }
        public void SurfaceBuilder(int _pointCountOnXAxis, int _pointCountOnYAxis);
        public SurfacePoint GetSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis);

    }
}
