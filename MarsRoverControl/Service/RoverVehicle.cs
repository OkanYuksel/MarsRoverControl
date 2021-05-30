using MarsRoverControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverControl.Service
{
    public class RoverVehicle : IRoverVehicle
    {
        public VehiclePositionProperty vehiclePositionProperty { get; set; }
        public ISurface surface { get; set; }
        public Guid roverId { get; set; }

        public RoverVehicle(int _locationOnTheXAxis, int _locationOnTheYAxis, int _vehicleDirectionState, ISurface _surface)
        {
            if (_surface == null)
            {
                // TODO yüzey alanı setlenmedi. Uyarı.
            }

            roverId = Guid.NewGuid();

            if (!_surface.VehicleMovePermissionControlForSurfacePoint(_locationOnTheXAxis, _locationOnTheYAxis, roverId))
            {
                // TODO belirtilen koordinatlar konumlanmak için uygun değil.
            }

            vehiclePositionProperty = new VehiclePositionProperty();
            vehiclePositionProperty.locationOnTheXAxis = _locationOnTheXAxis;
            vehiclePositionProperty.locationOnTheYAxis = _locationOnTheYAxis;
            vehiclePositionProperty.vehicleDirectionState = _vehicleDirectionState;
            surface = _surface;
            surface.VehicleRegistrationToSurface(this);

            //SurfacePoint surfacePoint = surface.GetSurfacePoint(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis);
            //if (surfacePoint != null)
            //{
            //    surfacePoint.PlaceVehicleToPoint(this);
            //}
        }

        public string TurnLeft()
        {
            if (vehiclePositionProperty.vehicleDirectionState > 0)
            {
                vehiclePositionProperty.vehicleDirectionState -= 1;
            }
            else
            {
                vehiclePositionProperty.vehicleDirectionState = DirectionService.GetDirectionCount() - 1;
            }
            return GetRoverPositionAndDirection();
        }

        public string TurnRight()
        {
            vehiclePositionProperty.vehicleDirectionState = (vehiclePositionProperty.vehicleDirectionState + 1) % DirectionService.GetDirectionCount();
            return GetRoverPositionAndDirection();
        }

        public string MoveForward()
        {
            if (vehiclePositionProperty != null)
            {
                switch (vehiclePositionProperty.vehicleDirectionState)
                {
                    case 0:
                        Console.WriteLine("Durum 0");
                        vehiclePositionProperty.locationOnTheYAxis += 1;
                        break;
                    case 1:
                        Console.WriteLine("Durum 1");
                        vehiclePositionProperty.locationOnTheXAxis += 1;
                        break;
                    case 2:
                        Console.WriteLine("Durum 2");
                        vehiclePositionProperty.locationOnTheYAxis -= 1;
                        break;
                    case 3:
                        Console.WriteLine("Durum 3");
                        vehiclePositionProperty.locationOnTheXAxis -= 1;
                        break;
                    default:
                        Console.WriteLine("Geçersiz durum.");
                        break;
                }
            }

            return GetRoverPositionAndDirection();
        }

        public string GetRoverPositionAndDirection()
        {
            return GetRoverPositionOnSurface() + " " + Enum.GetName(typeof(DirectionService.RoverDirections), vehiclePositionProperty.vehicleDirectionState);
        }

        public string GetRoverPositionOnSurface()
        {
            return vehiclePositionProperty.locationOnTheXAxis + " " + vehiclePositionProperty.locationOnTheYAxis;
        }

        public void RunCommands(List<char> commandList)
        {
            foreach (var command in commandList)
            {
                if (command.ToString() == "L")
                {
                    TurnLeft();
                }
                else if (command.ToString() == "R")
                {
                    TurnRight();
                }
                else if (command.ToString() == "M")
                {
                    MoveForward();
                }
            }
        }

    }
}
