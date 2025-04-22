/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

This is a purely UI based class, used to update the spell queue UI in game.
It makes no actual changes to what is in the spell queue, only updates the UI to match
when called by SpellManager.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellQueueSlot : MonoBehaviour
{
    public Image itemImage; // Picture that displays in the slot
    public Sprite emptySprite; // An empty sprite that shows a checkmark

    public void UpdateSpellQueueSlotUI(ItemSlot itemSlotToCopy)
    {
        // Updates the image displayed
        // If the slot is empty, set the sprite to the emptySprite
        if (itemSlotToCopy == null)
        {
            itemImage.sprite = emptySprite;
        }
        // Otherwise, set to match the slot in spellQueue
        else
        {
             itemImage.sprite = itemSlotToCopy.itemImage.sprite;
        }       
    }
}
