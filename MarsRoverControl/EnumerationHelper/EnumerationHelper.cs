using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public static class EnumerationHelper<T>
    where T : struct, Enum
{

    /// <summary>
    /// Checks if the character taken as a parameter exists in the Enum type
    /// </summary>
    /// <param name="character"></param>
    /// <returns>bool result</returns>
    public static bool EnumValidationWithChar(char character)
    {
        foreach (int enumItemValue in (int[])Enum.GetValues(typeof(T)))
        {
            if (character.ToString().ToUpper() == GetEnumItemName(enumItemValue))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Finds the name of the enumTypeValue that comes as a parameter.
    /// </summary>
    /// <param name="enumTypeValue"></param>
    /// <returns> string enum name</returns>
    public static string GetEnumItemName(int enumTypeValue)
    {
        string enumName = String.Empty;
        if (enumTypeValue > -1 && enumTypeValue < GetEnumItemsCount())
        {
            enumName = Enum.GetName(typeof(T), enumTypeValue);
        }
        return enumName;
    }

    /// <summary>
    /// This method searches how many items in the enum type
    /// </summary>
    /// <returns>integer count</returns>
    public static int GetEnumItemsCount()
    {
        return Enum.GetNames(typeof(T)).Length;
    }

    /// <summary>
    /// This method searching enum item with character and returning enum item value
    /// </summary>
    /// <param name="character"></param>
    /// <returns>int value</returns>
    public static int GetEnumItemValue(char character)
    {
        int directionState = -1;
        foreach (int enumItemValue in (int[])Enum.GetValues(typeof(T)))
        {
            if (character.ToString().ToUpper() == EnumerationHelper<T>.GetEnumItemName(enumItemValue))
            {
                directionState = enumItemValue;
                break;
            }
        }
        return directionState;
    }

}