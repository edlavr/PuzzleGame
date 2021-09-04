using UnityEngine;

namespace Interaction.InteractableSM
{
    public class InteractableStateActive : InteractableStateBase
    {
        public override void EnterState(Interactable interactable)
        {
            interactable._rb.useGravity = false;
            interactable._rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public override void UpdateState(Interactable interactable)
        {
            var _playerPosition = interactable.interactableDest.transform.position;
            var _interactablePosition = interactable.transform.position;
            float _currentDistance = Vector3.Distance(_playerPosition, _interactablePosition);
            if (_currentDistance > interactable.BreakDistance && !interactable.interactionControl._interactionBroken)
            {
                interactable.interactionControl._interactionBroken = true;
                return;
            }
            var _currentSpeed = Mathf.SmoothStep(interactable.MinSpeed, interactable.MaxSpeed, _currentDistance / interactable.MaxDistance);
            _currentSpeed *= Time.fixedDeltaTime;
            var _direction = _playerPosition - _interactablePosition;
            if (_currentDistance < 0.1)
            {
                _currentSpeed = 0f;
                _direction = Vector3.zero;
            }
            
            interactable._rb.velocity = _direction.normalized * _currentSpeed;
            
            //Rotation
            var _lookRot = Quaternion.LookRotation(interactable.playerCamera.transform.position - interactable._rb.position);
            _lookRot = Quaternion.Slerp(interactable.playerCamera.transform.rotation, _lookRot, interactable.RotSpeed * Time.fixedDeltaTime);
            interactable._rb.MoveRotation(_lookRot);
        }

        public override void ExitState(Interactable interactable)
        {
            interactable._rb.useGravity = true;
            interactable._rb.constraints = RigidbodyConstraints.None;
        }
    }
}
