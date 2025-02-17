/*
By LlamaAcademy!

An enumeration of events (which are basically like messages from Scratch)
Basically, a limited list of events that can be used to cause TriggerTransitions to activate

*/

namespace LlamAcademy.FSM
{
    public enum StateEvent
    {
        DetectPlayer,
        LostPlayer,
        RollImpact
    }
}