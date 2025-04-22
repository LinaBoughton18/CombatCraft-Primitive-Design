/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

This simple class has storage for an itemSO and a quantity.
It is used by enemies, who can store the type & quantity of the items they drop upon death (if any)

-----------------------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // This makes the class editable in the Inspector
public class ItemTuple
{
    public ItemSO item;
    public int quantity;
}
