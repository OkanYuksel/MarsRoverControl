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

                //rover 1

                VehiclePositionProperty vehiclePositionProperty = InputManagerService.GetRoverDefinition();

                RoverVehicle firstRover = new RoverVehicle(vehiclePositionProperty.locationOnTheXAxis, vehiclePositionProperty.locationOnTheYAxis, vehiclePositionProperty.vehicleDirectionState, surface);


                List<char> commands = InputManagerService.GetRoverCommands();

                CommandResult commandResult = surface.RunCommands(firstRover.roverId, firstRover.vehiclePositionProperty, commands);

                Console.WriteLine("commandResult rover 1   : " + JsonConvert.SerializeObject(commandResult));
           
                /////////////////////
                

                //rover 2

                VehiclePositionProperty vehiclePositionProperty2 = InputManagerService.GetRoverDefinition();

                RoverVehicle secondRover = new RoverVehicle(vehiclePositionProperty2.locationOnTheXAxis, vehiclePositionProperty2.locationOnTheYAxis, vehiclePositionProperty2.vehicleDirectionState, surface);

                List<char> commands2 = InputManagerService.GetRoverCommands();

                CommandResult commandResult2 = surface.RunCommands(secondRover.roverId, secondRover.vehiclePositionProperty, commands2);


                Console.WriteLine("commandResult rover 2   : " + JsonConvert.SerializeObject(commandResult2));
                Console.ReadLine();

                //////////////
                ///


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
                Console.WriteLine("Status Code : InternalError. An error has been occured: Exception : " + ex.ToString());
            }
        }


    }


}
