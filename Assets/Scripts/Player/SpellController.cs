using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    // Determines the state of the spell prep, active or not
    private bool spellPrepActive = false;

    // The 2 components that make up a spell, the spell must inclue both or it fails
    public bool component1 = false;
    public bool component2 = false;

    // The players inventory
    public int[] inventory;

    // The effect of the spell
    public GameObject spellEffect;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {
        // Will turn this into a finite state machine later once I learn how
        // Starts spell prep
        if (Input.GetKey(KeyCode.Mouse0) && !spellPrepActive) {
            //Debug.Log("Mouse held!");
            spellPrepActive = true;
        }

        // Releases Spell / Executes spell effect
        else if (!Input.GetKey(KeyCode.Mouse0) && spellPrepActive) {
            //Debug.Log("Spell Cast!");
            Instantiate(spellEffect, transform.position, spellEffect.transform.rotation);

            component1 = false;
            component2 = false;
            spellPrepActive = false;
        }

        // Occurs during spell prep
        else if (spellPrepActive) {
            if (Input.GetKey(KeyCode.Alpha1) && !component1 && (inventory[0] > 0)) {
                inventory[0]--;
                Debug.Log("Component 1 Added!");
                component1 = true;
            }
            if (Input.GetKey(KeyCode.Alpha2) && !component2 && (inventory[1] > 0)) {
                inventory[1]--;
                Debug.Log("Component 2 Added!");
                component2 = true;
            }
        }

    }
}