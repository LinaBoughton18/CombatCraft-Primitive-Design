using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is a purely UI based class, used to update the temporary spellqueue

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
