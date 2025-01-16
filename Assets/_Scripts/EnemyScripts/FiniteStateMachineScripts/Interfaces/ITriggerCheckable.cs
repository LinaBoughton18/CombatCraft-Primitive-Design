using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An interface for triggers on enemies,
// for aggro & striking distance triggers
public interface ITriggerCheckable
{
    bool isAggroed { get; set; }
    bool isWithinStrikingDistance { get; set; }

    void SetAggroStatus(bool isAggroed);
    void SetStrikingDistanceBool(bool isWithinStrikignDistance);
}
