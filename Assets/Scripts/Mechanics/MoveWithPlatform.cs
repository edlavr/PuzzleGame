using UnityEngine;

namespace Mechanics
{
    public class MoveWithPlatform : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Rigidbody platformRigidbody;

        private void OnTriggerStay(Collider other)
        {
            var _parent = other.transform.parent;
            if (!_parent) return;
            if (!_parent.CompareTag("Player")) return;
            Debug.Log(platformRigidbody.velocity);
            playerRigidbody.AddForce(platformRigidbody.velocity, ForceMode.VelocityChange);
        }
    }
}
