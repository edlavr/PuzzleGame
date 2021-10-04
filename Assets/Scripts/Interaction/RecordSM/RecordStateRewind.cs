using System.Linq;
using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateRewind : IStateMachine<Interactable>
    {
        private float _shaderValueDelta;
        public void EnterState(Interactable interactable)
        {
            interactable.isRewinding = true;
            interactable.ChangeInteractionState(interactable.InteractIdleState);
            interactable.gameObject.layer = 8;
            //interactable.BC.enabled = false;
            _shaderValueDelta = 1f / interactable.PointsInTime.Count;
            interactable.Material.SetInt("IsRewind",1);
        }

        public void UpdateState(Interactable interactable)
        {
            float _shaderValue = interactable.Material.GetFloat("Value");
            if (_shaderValue < 1)
            {
                interactable.Material.SetFloat("Value", _shaderValue + _shaderValueDelta);
            }
            interactable.RB.velocity = Vector3.zero;
            interactable.RB.angularVelocity = Vector3.zero;
            if (interactable.PointsInTime.Count != 0)
            {
                UpdateTransform(interactable);
            }
            else
            {
                interactable.ChangeRecordState(interactable.RecordIdleState);
            }
        }

        public void ExitState(Interactable interactable)
        {
            interactable.Material.SetInt("IsRewind",0);
            interactable.Material.SetColor("BaseColor", interactable.BaseColor);
            interactable.Material.SetColor("PaintColor", interactable.PaintColor);
            interactable.Material.SetFloat("Value", 0);
            interactable.PointsInTime.Clear();
            //interactable.BC.enabled = true;
            interactable.gameObject.layer = 7;
            interactable.isRewinding = false;
        }

        private void UpdateTransform(Interactable interactable)
        {
            var _pointInTime = interactable.PointsInTime[0];
            var _transform = interactable.transform;
            _transform.position = _pointInTime.Position;
            _transform.rotation = _pointInTime.Rotation;
            interactable.PointsInTime.RemoveAt(0);
        }
    }
}
