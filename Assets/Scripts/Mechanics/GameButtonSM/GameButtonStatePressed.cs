using UnityEngine;

namespace Mechanics.GameButtonSM
{
    public class GameButtonStatePressed : IStateMachine<GameButton>
    {
        public void EnterState(GameButton obj)
        {
            obj.ChangePosition = obj.Button.transform.position;
            obj.CurrentTime = 0;
        }

        public void UpdateState(GameButton obj)
        {
            if (obj.Button.transform.position != obj.PressedPosition)
            {
                obj.CurrentPosition = Vector3.Slerp(obj.ChangePosition, obj.PressedPosition, obj.CurrentTime / obj.PressTime);
                obj.CurrentTime += Time.fixedDeltaTime;
            }
            else
            {
                obj.CurrentPosition = obj.PressedPosition;
                obj.Button.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }

        public void ExitState(GameButton obj)
        {
            
        }
    }
}
