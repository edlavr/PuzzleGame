using UnityEngine;

namespace Interaction.PickupSM
{
    public class PickupStateActive : IStateMachine<Pickupable>
    {
        public void EnterState(Pickupable rewindableCube)
        {
            rewindableCube.RB.useGravity = false;
            rewindableCube.RB.constraints = RigidbodyConstraints.FreezeRotation;
            rewindableCube.gameObject.layer = 11;
        }

        public void UpdateState(Pickupable rewindableCube)
        {
            PositionRotationControl(rewindableCube);
        }

        public void ExitState(Pickupable rewindableCube)
        {
            rewindableCube.RB.useGravity = true;
            rewindableCube.RB.constraints = RigidbodyConstraints.None;
            rewindableCube.gameObject.layer = 7;
        }

        private void PositionRotationControl(Pickupable rewindableCube)
        {
            // Position
            var _playerPosition = rewindableCube.interactableDest.transform.position;
            var _interactablePosition = rewindableCube.transform.position;
            float _currentDistance = Vector3.Distance(_playerPosition, _interactablePosition);
            if (_currentDistance > rewindableCube.BreakDistance && !rewindableCube.interactionManager.InteractionBroken)
            {
                rewindableCube.interactionManager.InteractionBroken = true;
                return;
            }
            float _currentSpeed = Mathf.SmoothStep(rewindableCube.MinSpeed, rewindableCube.MaxSpeed, _currentDistance / rewindableCube.MaxDistance);
            _currentSpeed *= Time.fixedDeltaTime;
            var _direction = _playerPosition - _interactablePosition;
            if (_currentDistance < 0.1)
            {
                _currentSpeed = 0f;
                _direction = Vector3.zero;
            }
            rewindableCube.RB.velocity = _direction.normalized * _currentSpeed;
            
            // Rotation
            var _lookRot = Quaternion.LookRotation(rewindableCube.playerCamera.transform.position - rewindableCube.RB.position);
            _lookRot = Quaternion.Slerp(rewindableCube.playerCamera.transform.rotation, _lookRot, rewindableCube.RotSpeed * Time.fixedDeltaTime);
            rewindableCube.RB.MoveRotation(_lookRot);
        }
    }
}
