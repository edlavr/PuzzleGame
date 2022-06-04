using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateActive : IStateMachine<Recordable>
    {
        private Transform _transform;

        public void EnterState(Recordable recordable)
        {
            recordable.Material.shader = Shader.Find("HDRP/Lit");
            recordable.Material.SetColor("_BaseColor", Color.red);
            _transform = recordable.transform;
            recordable.PointsInTime.Add(new PointInTime(_transform.position, _transform.rotation));
        }

        public void UpdateState(Recordable recordable)
        {
            // float _shaderValue = interactableObj.Material.GetFloat("Value");
            // if (_shaderValue < 1)
            // {
            //     interactableObj.Material.SetFloat("Value", _shaderValue + Time.deltaTime * interactableObj.ColorSpeed);
            // }
            _transform = recordable.transform;
            PointInTime _currentPointInTime = new PointInTime(_transform.position, _transform.rotation);
            if (!IsSamePoint(recordable.PointsInTime[0], _currentPointInTime))
            { 
                recordable.PointsInTime.Insert(0, _currentPointInTime);
            }
        }

        private bool IsSamePoint(PointInTime p1, PointInTime p2)
        {
            return p1.Position == p2.Position && p1.Rotation == p2.Rotation;
        }

        public void ExitState(Recordable recordable)
        {
            
            //interactableObj.Material.SetColor("BaseColor", interactableObj.PaintColor);
            //interactableObj.Material.SetColor("PaintColor", interactableObj.BaseColor);
            //interactableObj.Material.SetFloat("Value", 0);
        }
    }
}
