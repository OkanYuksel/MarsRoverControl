﻿using System;

namespace MarsRoverControl.Service
{
    public static class DirectionService
    {
        /// <summary>
        /// It is indexed clockwise starting from the north. If want to add intermediate directions, attention should be to this.
        /// </summary>
        public enum RoverDirections { N, E, S, W };

        /// <summary>
        /// Returns the direction count.
        /// </summary>
        /// <returns>integer count</returns>
        public static int GetDirectionCount()
        {
            return Enum.GetNames(typeof(DirectionService.RoverDirections)).Length;
        }

        /// <summary>
        /// Finds the character in directions and returns its value.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>integer direction state</returns>
        public static int GetDirectionState(char direction)
        {
            int directionState = -1;
            foreach (RoverDirections roverDirection in (RoverDirections[])Enum.GetValues(typeof(RoverDirections)))
            {
                if (direction.ToString() == GetDirectionName((int)roverDirection))
                {
                    directionState = (int)roverDirection;
                    break;
                }
            }
            return directionState;
        }

        /// <summary>
        /// Checks if the character is valid for directions.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>bool result</returns>
        public static bool DirectionValidation(char direction)
        {
            return GetDirectionState(direction) > -1;
        }

        /// <summary>
        /// Finds the name of the direction state that comes as a parameter.
        /// </summary>
        /// <param name="directionState"></param>
        /// <returns> string direction name</returns>
        public static string GetDirectionName(int directionState)
        {
            string directionName = "";
            if (directionState < GetDirectionCount())
            {
                directionName = Enum.GetName(typeof(DirectionService.RoverDirections), directionState);
            }
            return directionName;
        }
    }
}
