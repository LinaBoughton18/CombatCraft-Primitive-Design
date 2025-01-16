using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class SpellEffectMovement : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}