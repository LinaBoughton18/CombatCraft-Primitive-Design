using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellShapeSO : ScriptableObject, ISpellShape
{
    // The name/ranking of the shape
    public GrandPropertyList.Shape shape;

    public ISpellShape concreteShapeBehavior;

    public void Execute()
    {
        
    }
}