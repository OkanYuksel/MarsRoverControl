using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.EnumerationHelper
{
    //public static class EnumerationHelper
    //{

    //    public enum Commands { L, R, M };

    //    /// <summary>
    //    /// Validates the character used for the command.
    //    /// </summary>
    //    /// <param name="command"></param>
    //    /// <returns>bool result</returns>
    //    public static bool CommandValidation<T>(T source, char command)
    //    {
    //        foreach (int roverDirection in (int[])Enum.GetValues(typeof(T)))
    //        {
    //            if (command.ToString().ToUpper() == GetDirectionName(source, directionState: (int)roverDirection))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;

    //    }

    //    /// <summary>
    //    /// Finds the name of the direction state that comes as a parameter.
    //    /// </summary>
    //    /// <param name="directionState"></param>
    //    /// <returns> string direction name</returns>
    //    public static string GetDirectionName<T>(T source, int directionState)
    //    {
    //        string directionName = "";
    //        if (directionState < GetDirectionCount(source))
    //        {
    //            directionName = Enum.GetName(typeof(T), directionState);
    //        }
    //        return directionName;
    //    }

    //    public static int GetDirectionCount<T>(T source)
    //    {
    //        return Enum.GetNames(typeof(T)).Length;
    //    }

    //    public static Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
    //    {
    //        var result = new Dictionary<int, string>();
    //        var values = Enum.GetValues(typeof(T));

    //        foreach (int item in values)
    //            result.Add(item, Enum.GetName(typeof(T), item));
    //        return result;
    //    }
    //}
}
