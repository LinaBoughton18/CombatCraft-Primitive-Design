//---------------------------------------- BY LINA ----------------------------------------//
//-----------------------------------------------------------------------------------------//

// This is a public class that holds reusable debug messages, such a printing out all the items in an array
// Currently only contains one function, but can be expanded upon as needed

//-----------------------------------------------------------------------------------------//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static GrandPropertyList;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class StockDebug
{
    // Prints out the contents of an array's attributes. As input, takes the array, the attribute or variable to print out,
    // the text that starts the message, and if null items should be printed or not.
    // Lists need to be called with the   .ToArray() function
    // Example of proper call: StockDebug.PrintArray(itemList.ToArray(), item => item.itemName, "Item List = ", false);
    public static void PrintArray<T, TProperty>(T[] array, Func<T, TProperty> propertySelector, string startingText, bool printNulls)
    {
        string finalMessage = startingText;
        foreach (var i in array)
        {
            if (i == null)
            {
                if (printNulls && printNulls)
                {
                    finalMessage += "null item, ";
                }
            }
            else if (propertySelector == null)
            {
                if (printNulls)
                {
                    finalMessage += "null property, ";
                }
            }
            else
            {
                finalMessage += propertySelector(i) + ", ";
            }
        }
        Debug.Log(finalMessage);
    }

}
