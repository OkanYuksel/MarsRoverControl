using MarsRoverControl.Consts;
using MarsRoverControl.Interfaces;
using MarsRoverControl.Models;
using MarsRoverControl.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverControl
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(Messages.START_MESSAGE);

                SurfaceProperty surfaceProperty = InputManagerService.GetTheSurfaceSize();

                ServiceProvider serviceProvider = InjectionServiceProvider.Builder();
                ISurface surface = serviceProvider.GetService<ISurface>();

                surface.SurfaceBuilder(surfaceProperty.width, surfaceProperty.height);

                VehiclePositionProperty vehiclePositionProperty = InputManagerService.GetRoverDefinition();

                RoverVehicle firstRover = new RoverVehicle(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis, vehiclePositionProperty.vehicleDirectionState, surface);

                do
                {
                    List<char> commands = InputManagerService.GetRoverCommands();

                    CommandResult simulationResult = surface.RunCommands(firstRover.roverId, firstRover.vehiclePositionProperty, commands);

                    Console.WriteLine("SimulationForTheCommands : " + JsonConvert.SerializeObject(simulationResult));
                    Console.ReadLine();

                } while (true);



                //firstRover.RunCommands(commands);

                //VehiclePositionProperty vehiclePositionProperty2 = InputManagerService.GetRoverDefinition();

                //RoverVehicle secondRover = new RoverVehicle(vehiclePositionProperty2.locationOnTheXAxis, vehiclePositionProperty2.locationOnTheYAxis, vehiclePositionProperty2.vehicleDirectionState, surface);

                //List<char> commands2 = InputManagerService.GetRoverCommands();

                //secondRover.RunCommands(commands2);



                //firstRover.SetSurfaceValue(3);
                //Console.WriteLine(firstRover.surface.surfaceCode);
                //Console.WriteLine(secondRover.surface.surfaceCode);

                //firstRover.SetSurfaceValue(5);
                //Console.WriteLine(firstRover.surface.surfaceCode);
                //Console.WriteLine(secondRover.surface.surfaceCode);

                //secondRover.SetSurfaceValue(7);
                //Console.WriteLine(firstRover.surface.surfaceCode);
                //Console.WriteLine(secondRover.surface.surfaceCode);

                //firstRover.SetSurfaceValue(2);
                //Console.WriteLine(firstRover.surface.surfaceCode);
                //Console.WriteLine(secondRover.surface.surfaceCode);

                //secondRover.SetSurfaceValue(4);
                //Console.WriteLine(firstRover.surface.surfaceCode);
                //Console.WriteLine(secondRover.surface.surfaceCode);

                //Console.WriteLine(firstRover.TurnLeft());
                //Console.WriteLine(firstRover.TurnLeft());
                //Console.WriteLine(firstRover.TurnLeft());
                //Console.WriteLine(firstRover.TurnLeft());
                //Console.WriteLine(firstRover.TurnLeft());
                //Console.WriteLine(firstRover.TurnLeft());
                //Console.WriteLine(firstRover.TurnRight());
                //Console.WriteLine(firstRover.TurnRight());
                //Console.WriteLine(firstRover.TurnRight());
                //Console.WriteLine(firstRover.TurnRight());
                //Console.WriteLine(firstRover.TurnRight());
                //Console.WriteLine(firstRover.TurnRight());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }


}
