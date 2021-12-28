using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateActive : IStateMachine<InteractableObj>
    {
        private Transform _transform;

        public void EnterState(InteractableObj interactableObj)
        {
            _transform = interactableObj.transform;
            interactableObj.PointsInTime.Add(new PointInTime(_transform.position, _transform.rotation));
        }

        public void UpdateState(InteractableObj interactableObj)
        {
            // float _shaderValue = interactableObj.Material.GetFloat("Value");
            // if (_shaderValue < 1)
            // {
            //     interactableObj.Material.SetFloat("Value", _shaderValue + Time.deltaTime * interactableObj.ColorSpeed);
            // }
            _transform = interactableObj.transform;
            PointInTime _currentPointInTime = new PointInTime(_transform.position, _transform.rotation);
            if (!IsSamePoint(interactableObj.PointsInTime[0], _currentPointInTime))
            { 
                interactableObj.PointsInTime.Insert(0, _currentPointInTime);
            }
        }

        private bool IsSamePoint(PointInTime p1, PointInTime p2)
        {
            return p1.Position == p2.Position && p1.Rotation == p2.Rotation;
        }

        public void ExitState(InteractableObj interactableObj)
        {
            //interactableObj.Material.SetColor("BaseColor", interactableObj.PaintColor);
            //interactableObj.Material.SetColor("PaintColor", interactableObj.BaseColor);
            //interactableObj.Material.SetFloat("Value", 0);
        }
    }
}
