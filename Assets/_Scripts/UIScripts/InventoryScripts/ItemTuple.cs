using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is currently used for enemies,
// They can use this class to store the type & quantity of items they drop upon death

[Serializable] // This makes the class editable in the Inspector
public class ItemTuple
{
    public ItemSO item;
    public int quantity;
}
