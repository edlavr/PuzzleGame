using UnityEngine;

namespace Mechanics
{
    public class MoveWithPlatform : MonoBehaviour
    {
        [SerializeField] private CharacterController playerCharacterController;
        [SerializeField] private Rigidbody platformRigidbody;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
                playerCharacterController.Move(platformRigidbody.velocity.normalized * Time.fixedDeltaTime / 2);
        }
    }
}
