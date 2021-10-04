using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateActive : IStateMachine<Interactable>
    {
        private Transform _transform;

        public void EnterState(Interactable interactable)
        {
            _transform = interactable.transform;
            interactable.PointsInTime.Add(new PointInTime(_transform.position, _transform.rotation));
        }

        public void UpdateState(Interactable interactable)
        {
            float _shaderValue = interactable.Material.GetFloat("Value");
            if (_shaderValue < 1)
            {
                interactable.Material.SetFloat("Value", _shaderValue + Time.deltaTime * interactable.ColorSpeed);
            }
            _transform = interactable.transform;
            PointInTime _currentPointInTime = new PointInTime(_transform.position, _transform.rotation);
            if (!IsSamePoint(interactable.PointsInTime[0], _currentPointInTime))
            { 
                interactable.PointsInTime.Insert(0, _currentPointInTime);
            }
        }

        private bool IsSamePoint(PointInTime p1, PointInTime p2)
        {
            return p1.Position == p2.Position && p1.Rotation == p2.Rotation;
        }

        public void ExitState(Interactable interactable)
        {
            interactable.Material.SetColor("BaseColor", interactable.PaintColor);
            interactable.Material.SetColor("PaintColor", interactable.BaseColor);
            interactable.Material.SetFloat("Value", 0);
        }
    }
}
