using UnityEngine;

namespace LlamAcademy.Sensors
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class ImpactSensor : MonoBehaviour
    {
        public delegate void CollisionEvent(Collision2D Collision);
        public event CollisionEvent OnCollision;

        public void OnCollisionEnter2D(Collision2D Collision)
        {
            OnCollision?.Invoke(Collision);
        }
    }
}
