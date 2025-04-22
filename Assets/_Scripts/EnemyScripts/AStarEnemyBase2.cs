/*---------------------------------------- BY LINA ----------------------------------------
------------------------------- CURRENTLY UNUSED IN GAME ----------------------------------

An older, now unused implementation of an enemy.
Is able to pathfind to the player using the A* Pathfinding Project &store enemySO data.

-----------------------------------------------------------------------------------------*/

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEnemyBase2 : MonoBehaviour
{
    // ENEMY DATA -------------------------------------------------//
    public EnemySO enemySO; // The SO we pull our data from
    public int currentHealth; // The enemy's CURRENT health

    #region //------COMPONENTS AND SCENE OBJECTS------//
    // Scene Objects
    private GameObject player;

    // Enemy Components & Child Objects
    [SerializeField] private GameObject spriteObject; // Assigned in Unity editor
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider;

    private AIDestinationSetter destinationSetter;

    #endregion -------------------------------------------------//

    #region ENEMY SETUP ------------------------------------------//
    private void Awake()
    {
        // Set scene objects & components
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");

        // Set player as destination
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = player.transform;

        // Set currentHealth to maxHealth
        currentHealth = enemySO.maxHealth;
    }

    public void PassEnemySOData(EnemySO enemySO)
    {
        // Passes along the proper itemSO
        this.enemySO = enemySO;

        // Sets the sprite & proper size
        spriteRenderer.sprite = enemySO.enemySprite;
        FitSpriteToCollider();
    }

    // Alters the size of the sprite to fit within the borders of the collider
    private void FitSpriteToCollider()
    {
        // Get the size of the sprite & box collider
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size; // in world unity
        Vector2 colliderSize = boxCollider.size;

        // Calculate scale needed to fit sprite within collider
        float scaleX = colliderSize.x / spriteSize.x;
        float scaleY = colliderSize.y / spriteSize.y;

        // Applies scale to the sprite while maintaining the aspect ratio
        float scale = Mathf.Min(scaleX, scaleY);
        spriteObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    #endregion -------------------------------------------------//
}