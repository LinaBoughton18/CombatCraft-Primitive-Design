/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

A simple interface script, gives all spell shapes a common "trigger" (the execute function),
which can be called regardless of which shape is used.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellShape
{
    // Executes shape-based behaviour
    void Execute();
}
