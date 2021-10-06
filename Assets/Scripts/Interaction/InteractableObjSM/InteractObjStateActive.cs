using UnityEngine;

namespace Interaction.InteractableObjSM
{
    public class InteractObjStateActive : IStateMachine<InteractableObj>
    {
        public void EnterState(InteractableObj interactableObj)
        {
            interactableObj.RB.useGravity = false;
            interactableObj.RB.constraints = RigidbodyConstraints.FreezeRotation;
            interactableObj.gameObject.layer = 11;
        }

        public void UpdateState(InteractableObj interactableObj)
        {
            PositionRotationControl(interactableObj);
        }

        public void ExitState(InteractableObj interactableObj)
        {
            interactableObj.RB.useGravity = true;
            interactableObj.RB.constraints = RigidbodyConstraints.None;
            interactableObj.gameObject.layer = 7;
        }

        private void PositionRotationControl(InteractableObj interactableObj)
        {
            // Position
            var _playerPosition = interactableObj.interactableDest.transform.position;
            var _interactablePosition = interactableObj.transform.position;
            float _currentDistance = Vector3.Distance(_playerPosition, _interactablePosition);
            if (_currentDistance > interactableObj.BreakDistance && !interactableObj.interactionControl.InteractionBroken)
            {
                interactableObj.interactionControl.InteractionBroken = true;
                return;
            }
            var _currentSpeed = Mathf.SmoothStep(interactableObj.MinSpeed, interactableObj.MaxSpeed, _currentDistance / interactableObj.MaxDistance);
            _currentSpeed *= Time.fixedDeltaTime;
            var _direction = _playerPosition - _interactablePosition;
            if (_currentDistance < 0.1)
            {
                _currentSpeed = 0f;
                _direction = Vector3.zero;
            }
            interactableObj.RB.velocity = _direction.normalized * _currentSpeed;
            
            // Rotation
            var _lookRot = Quaternion.LookRotation(interactableObj.playerCamera.transform.position - interactableObj.RB.position);
            _lookRot = Quaternion.Slerp(interactableObj.playerCamera.transform.rotation, _lookRot, interactableObj.RotSpeed * Time.fixedDeltaTime);
            interactableObj.RB.MoveRotation(_lookRot);
        }
    }
}
