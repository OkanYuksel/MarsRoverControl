using MarsRoverControl.Service;
using System;

namespace MarsRoverControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RoverVehicle firstRover = new RoverVehicle(1,2,0);

            Console.WriteLine(firstRover.TurnLeft());
            Console.WriteLine(firstRover.TurnLeft());
            Console.WriteLine(firstRover.TurnLeft());
            Console.WriteLine(firstRover.TurnLeft());
            Console.WriteLine(firstRover.TurnLeft());
            Console.WriteLine(firstRover.TurnLeft());
            Console.WriteLine(firstRover.TurnRight());
            Console.WriteLine(firstRover.TurnRight());
            Console.WriteLine(firstRover.TurnRight());
            Console.WriteLine(firstRover.TurnRight());
            Console.WriteLine(firstRover.TurnRight());
            Console.WriteLine(firstRover.TurnRight());


        }
    }
}
