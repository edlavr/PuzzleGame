using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateRewind : IStateMachine<Recordable>
    {
        //private float _shaderValueDelta;
        public void EnterState(Recordable recordable)
        {
            recordable.Material.shader = Shader.Find("HDRP/Lit");
            recordable.Material.SetColor("_BaseColor", Color.blue);
            recordable.IsRewinding = true;
            recordable.ChangePickupState(recordable.PickupIdleState);
            recordable.gameObject.layer = 8;
            //interactable.BC.enabled = false;
            //_shaderValueDelta = 1f / interactableObj.PointsInTime.Count;
            //interactableObj.Material.SetInt("IsRewind",1);
        }

        public void UpdateState(Recordable recordable)
        {
            // float _shaderValue = interactableObj.Material.GetFloat("Value");
            // if (_shaderValue < 1)
            // {
            //     interactableObj.Material.SetFloat("Value", _shaderValue + _shaderValueDelta);
            // }
            recordable.RB.velocity = Vector3.zero;
            recordable.RB.angularVelocity = Vector3.zero;
            if (recordable.PointsInTime.Count != 0)
            {
                UpdateTransform(recordable);
            }
            else
            {
                recordable.ChangeRecordState(recordable.RecordIdleState);
            }
        }

        public void ExitState(Recordable recordable)
        {
            //interactableObj.Material.shader = Shader.Find("HDRP/Lit");
            //interactableObj.Material.SetColor("_BaseColor", Color.black);
            //interactableObj.Material.SetInt("IsRewind",0);
            //interactableObj.Material.SetColor("BaseColor", interactableObj.BaseColor);
            //interactableObj.Material.SetColor("PaintColor", interactableObj.PaintColor);
            //interactableObj.Material.SetFloat("Value", 0);
            recordable.PointsInTime.Clear();
            //interactable.BC.enabled = true;
            recordable.gameObject.layer = 7;
            recordable.IsRewinding = false;
        }

        private void UpdateTransform(Recordable recordable)
        {
            var _pointInTime = recordable.PointsInTime[0];
            var _transform = recordable.transform;
            _transform.position = _pointInTime.Position;
            _transform.rotation = _pointInTime.Rotation;
            recordable.PointsInTime.RemoveAt(0);
        }
    }
}
