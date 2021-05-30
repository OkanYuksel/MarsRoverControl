using MarsRoverControl.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverControl.Service
{
    public static class InputManagerService
    {
        /// <summary>
        /// Gets the width & height parameters to create surface from the user. And then doing validation for the parameters.
        /// </summary>
        /// <returns>SurfaceProperty object</returns>
        public static SurfaceProperty GetTheSurfaceSize()
        {
            int xSize = 0;
            int ySize = 0;
            bool enteringCompleted = false;
            do
            {
                string line = Console.ReadLine();
                string[] fields = line.Split(" ");

                if (fields != null)
                {
                    if (fields.Length == 2)
                    {
                        int val;
                        bool kontrol = int.TryParse(fields.First(), out val);
                        if (kontrol)
                        {
                            xSize = val;
                        }

                        int val2;
                        bool kontrol2 = int.TryParse(fields.Last(), out val2);
                        if (kontrol2)
                        {
                            ySize = val2;
                        }

                        if (kontrol && kontrol2)
                        {
                            enteringCompleted = true;
                        }
                    }
                }

                if (enteringCompleted == false)
                {
                    Console.WriteLine(Messages.WRONG_INPUT);
                }

            } while (enteringCompleted == false);

            return new SurfaceProperty { width = xSize, height = ySize };
        }

        /// <summary>
        /// Gets the initial positioning parameters of the rover vehicle from the user.
        /// </summary>
        /// <returns>VehiclePositionProperty object</returns>
        public static VehiclePositionProperty GetRoverDefinition()
        {
            Console.WriteLine(Messages.ENTER_ROVER_POSITION_MESSAGE);
            int xPosition = 0;
            int yPosition = 0;
            int directionState = -1;
            bool enteringCompleted = false;
            do
            {
                string line = Console.ReadLine();
                string[] fields = line.Split(" ");

                if (fields != null)
                {
                    if (fields.Length == 3)
                    {
                        int val;
                        bool kontrol = int.TryParse(fields.First(), out val);
                        if (kontrol)
                        {
                            xPosition = val;
                        }

                        int val2;
                        bool kontrol2 = int.TryParse(fields[1], out val2);
                        if (kontrol2)
                        {
                            yPosition = val2;
                        }

                        Char val3;
                        bool kontrol3 = Char.TryParse(fields.Last(), out val3);
                        if (kontrol3)
                        {
                            int state = DirectionService.GetDirectionState(val3);
                            if (state > -1)
                            {
                                directionState = state;
                            }
                        }


                        if (kontrol && kontrol2 && directionState > -1)
                        {
                            enteringCompleted = true;
                        }
                    }
                }

                if (enteringCompleted == false)
                {
                    Console.WriteLine(Messages.WRONG_INPUT);
                }

            } while (enteringCompleted == false);

            return new VehiclePositionProperty { locationOnTheXAxis = xPosition, locationOnTheYAxis = yPosition, vehicleDirectionState = directionState };
        }

        /// <summary>
        /// It takes parameters from the user to move the rover vehicle.
        /// </summary>
        /// <returns>Command character list</returns>
        public static List<char> GetRoverCommands()
        {
            Console.WriteLine(Messages.ENTER_MOVEMENT_COMMANDS_MESSAGE);

            List<char> commandList = new List<char>();

            bool enteringCompleted = false;
            do
            {
                string line = Console.ReadLine();
                char[] fields = line.ToUpper().ToArray();

                if (fields != null)
                {
                    if (fields.Length > 0)
                    {
                        bool isHasNonValidChar = false;
                        foreach (char command in fields)
                        {
                            if (DirectionService.CommandValidation(command))
                            {
                                commandList.Add(command);
                            }
                            else
                            {
                                isHasNonValidChar = true;
                            }
                        }

                        if (isHasNonValidChar == false && commandList.Count > 0)
                        {
                            enteringCompleted = true;
                        }
                    }
                }

                if (enteringCompleted == false)
                {
                    Console.WriteLine(Messages.WRONG_INPUT);
                }

            } while (enteringCompleted == false);

            return commandList;
        }
    }
}
