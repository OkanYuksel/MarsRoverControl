﻿using MarsRoverControl.Consts;
using MarsRoverControl.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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

            //It takes 2 integer parameters for the surface size. Otherwise, it will ask again.
            do
            {
                string line = Console.ReadLine();
                string[] fields = line.Split(" ");

                if (fields != null)
                {
                    if (fields.Length == 2)
                    {
                        int xSizeValue;
                        bool controlForXSize = int.TryParse(fields.First(), out xSizeValue);
                        if (controlForXSize)
                        {
                            xSize = xSizeValue;
                        }

                        int ySizeValue;
                        bool controlForYSize = int.TryParse(fields.Last(), out ySizeValue);
                        if (controlForYSize)
                        {
                            ySize = ySizeValue;
                        }

                        if (controlForXSize && controlForYSize)
                        {
                            if (xSize > 0 && ySize > 0)
                            {
                                enteringCompleted = true;
                            }
                            else
                            {
                                Console.WriteLine(Messages.INVALID_SURFACE_WIDTH_PARAMETERS_MESSAGE);
                            }
                        }
                    }
                }

                if (enteringCompleted == false)
                {
                    Console.WriteLine(Messages.INCORRECT_INPUT_MESSAGE);
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

            //For the vehicle position, it takes 2 integer and 1 char parameters. Otherwise, it will ask again.
            do
            {
                string line = Console.ReadLine();
                string[] fields = line.Split(" ");

                if (fields != null)
                {
                    if (fields.Length == 3)
                    {
                        int xPositionValue;
                        bool controlForXPosition = int.TryParse(fields.First(), out xPositionValue);
                        if (controlForXPosition)
                        {
                            xPosition = xPositionValue;
                        }

                        int yPositionValue;
                        bool controlForYPosition = int.TryParse(fields[1], out yPositionValue);
                        if (controlForYPosition)
                        {
                            yPosition = yPositionValue;
                        }

                        Char directionValue;
                        bool controlForDirection = Char.TryParse(fields.Last(), out directionValue);
                        if (controlForDirection)
                        {
                            int value = EnumerationHelper<RoverDirections>.GetEnumItemValue(directionValue);
                            if (value > -1)
                            {
                                directionState = value;
                            }
                        }


                        if (controlForXPosition && controlForYPosition && directionState > -1)
                        {
                            enteringCompleted = true;
                        }
                    }
                }

                if (enteringCompleted == false)
                {
                    Console.WriteLine(Messages.INCORRECT_INPUT_MESSAGE);
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

            //asks for command array of valid commands to move the vehicle.
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
                            if (EnumerationHelper<Commands>.EnumValidationWithChar(command))
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
                    Console.WriteLine(Messages.INCORRECT_INPUT_MESSAGE);
                }

            } while (enteringCompleted == false);

            return commandList;
        }
    }
}
