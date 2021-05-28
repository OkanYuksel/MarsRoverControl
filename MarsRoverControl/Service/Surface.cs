using MarsRoverControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverControl.Service
{
    public class Surface : ISurface
    {
        public List<SurfacePoint> surfacePointList { get; set; }

        public int surfaceCode { get; set; }

        public void SurfaceBuilder(int _pointCountOnXAxis, int _pointCountOnYAxis)
        {
            List<SurfacePoint> surfacePoints = new List<SurfacePoint>();

            for (var y = 0; y <= _pointCountOnYAxis; y += 1)
            {
                for (var x = 0; x <= _pointCountOnXAxis; x += 1)
                {
                    surfacePoints.Add(new SurfacePoint(x, y));
                }
            }

            surfacePointList = surfacePoints;
        }

        public SurfacePoint GetSurfacePoint(int _locationOnTheXAxis, int _locationOnTheYAxis)
        {
            if (this.surfacePointList == null)
            {
                return null;
            }

            return this.surfacePointList.Where(x => x.locationOnTheXAxis == _locationOnTheXAxis && x.locationOnTheYAxis == _locationOnTheYAxis).FirstOrDefault();
        }
    }
}
