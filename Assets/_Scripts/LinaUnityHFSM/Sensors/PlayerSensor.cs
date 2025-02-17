using UnityEngine;

// Detects if the player is in range of the sensor or not
// Requires setting up physics layers in Unity,
// so that only certain layers can collide
// Set up to specifically detect the player, nothing else

namespace LlamAcademy.Sensors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerSensor : MonoBehaviour
    {
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);
        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;

        // Requires a CircleCollider2D component!
        // When the player enters, raises a PlayerEnterEvent
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                OnPlayerEnter?.Invoke(player.transform);
            }
        }

        // When the player leaves, raises a PlayerExitEvent
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                OnPlayerExit?.Invoke(other.transform.position);
            }
        }
    }
}