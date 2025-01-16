using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We rid ourselves of the "MonoBehaviour" and replace with EnemyBase, since we're
// directly inheriting the enemy class (which includes MonoBehaviour by extension)
// Note that variables like MaxHealth show up in the Unity Editor even though they're
// not directly in this class. They're inherited from EnemyBase
public class Candlehead : EnemyBase
{
    
}
