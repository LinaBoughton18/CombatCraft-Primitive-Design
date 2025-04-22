/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

Stores information about spell properties (conditions).

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] // Allows you to create instances of this SO by
// clicking "Create" in Unity, so we can easily create additional objects
public class ConditionSO : ScriptableObject
{
    public string conditionName;

    // The sprite associated with the damage type
    public Sprite sprite;
    // The color associated with the danage type
    public Color color;
}
