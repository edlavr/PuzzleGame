using UnityEngine;

namespace Interaction.InteractSM
{
    public class InteractObjStateActive : IStateMachine<Interactable>
    {
        public void EnterState(Interactable interactable)
        {
            interactable.RB.useGravity = false;
            interactable.RB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void UpdateState(Interactable interactable)
        {
            PositionRotationControl(interactable);
        }

        public void ExitState(Interactable interactable)
        {
            interactable.RB.useGravity = true;
            interactable.RB.constraints = RigidbodyConstraints.None;
        }

        private void PositionRotationControl(Interactable interactable)
        {
            // Position
            var _playerPosition = interactable.interactableDest.transform.position;
            var _interactablePosition = interactable.transform.position;
            float _currentDistance = Vector3.Distance(_playerPosition, _interactablePosition);
            if (_currentDistance > interactable.BreakDistance && !interactable.interactionControl.InteractionBroken)
            {
                interactable.interactionControl.InteractionBroken = true;
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
            interactable.RB.velocity = _direction.normalized * _currentSpeed;
            
            // Rotation
            var _lookRot = Quaternion.LookRotation(interactable.playerCamera.transform.position - interactable.RB.position);
            _lookRot = Quaternion.Slerp(interactable.playerCamera.transform.rotation, _lookRot, interactable.RotSpeed * Time.fixedDeltaTime);
            interactable.RB.MoveRotation(_lookRot);
        }
    }
}
