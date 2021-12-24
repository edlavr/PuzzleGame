using System.Linq;
using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateRewind : IStateMachine<InteractableObj>
    {
        private float _shaderValueDelta;
        public void EnterState(InteractableObj interactableObj)
        {
            interactableObj.IsRewinding = true;
            interactableObj.ChangeInteractionState(interactableObj.InteractIdleState);
            interactableObj.gameObject.layer = 8;
            //interactable.BC.enabled = false;
            _shaderValueDelta = 1f / interactableObj.PointsInTime.Count;
            interactableObj.Material.SetInt("IsRewind",1);
        }

        public void UpdateState(InteractableObj interactableObj)
        {
            float _shaderValue = interactableObj.Material.GetFloat("Value");
            if (_shaderValue < 1)
            {
                interactableObj.Material.SetFloat("Value", _shaderValue + _shaderValueDelta);
            }
            interactableObj.RB.velocity = Vector3.zero;
            interactableObj.RB.angularVelocity = Vector3.zero;
            if (interactableObj.PointsInTime.Count != 0)
            {
                UpdateTransform(interactableObj);
            }
            else
            {
                interactableObj.ChangeRecordState(interactableObj.RecordIdleState);
            }
        }

        public void ExitState(InteractableObj interactableObj)
        {
            //interactableObj.Material.SetInt("IsRewind",0);
            //interactableObj.Material.SetColor("BaseColor", interactableObj.BaseColor);
            //interactableObj.Material.SetColor("PaintColor", interactableObj.PaintColor);
            //interactableObj.Material.SetFloat("Value", 0);
            interactableObj.PointsInTime.Clear();
            //interactable.BC.enabled = true;
            interactableObj.gameObject.layer = 7;
            interactableObj.IsRewinding = false;
        }

        private void UpdateTransform(InteractableObj interactableObj)
        {
            var _pointInTime = interactableObj.PointsInTime[0];
            var _transform = interactableObj.transform;
            _transform.position = _pointInTime.Position;
            _transform.rotation = _pointInTime.Rotation;
            interactableObj.PointsInTime.RemoveAt(0);
        }
    }
}
