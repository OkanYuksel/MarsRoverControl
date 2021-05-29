using MarsRoverControl.Interfaces;
using MarsRoverControl.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarsRoverControl
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                ServiceProvider serviceProvider = InjectionServiceProvider.Builder();
                ISurface surface = serviceProvider.GetService<ISurface>();

                surface.SurfaceBuilder(5, 5);

                RoverVehicle firstRover = new RoverVehicle(1, 2, 0, surface);

                RoverVehicle secondRover = new RoverVehicle(1, 3, 0, surface);


                firstRover.SetSurfaceValue(3);
                Console.WriteLine(firstRover.surface.surfaceCode);
                Console.WriteLine(secondRover.surface.surfaceCode);

                firstRover.SetSurfaceValue(5);
                Console.WriteLine(firstRover.surface.surfaceCode);
                Console.WriteLine(secondRover.surface.surfaceCode);

                secondRover.SetSurfaceValue(7);
                Console.WriteLine(firstRover.surface.surfaceCode);
                Console.WriteLine(secondRover.surface.surfaceCode);

                firstRover.SetSurfaceValue(2);
                Console.WriteLine(firstRover.surface.surfaceCode);
                Console.WriteLine(secondRover.surface.surfaceCode);

                secondRover.SetSurfaceValue(4);
                Console.WriteLine(firstRover.surface.surfaceCode);
                Console.WriteLine(secondRover.surface.surfaceCode);

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
