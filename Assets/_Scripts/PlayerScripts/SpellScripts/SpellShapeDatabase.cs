/*---------------------------------------- BY LINA ----------------------------------------
------------------------------- CURRENTLY UNUSED IN GAME ----------------------------------

Stores the spellShapeSO's in a database. Can later be used to reference spellshapes at runtime (if necessary)

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A list for storing all our spell shapes :)

//[CreateAssetMenu(menuName = "SpellShape/Database")]
public class SpellShapeDatabase : ScriptableObject
{
    // Our list of spell shapes (where the spells will be managed in Unity Editor)
    [SerializeField] private List<SpellShapeSO> spellShapes = new List<SpellShapeSO>();
    
    // The dictionary where the spells will be stored (& looked up at runtime)
    private Dictionary<string, SpellShapeSO> spellShapeDict;
}
