using UnityEngine;

namespace Mechanics.GameButtonSM
{
    public class GameButtonStateIdle : IStateMachine<GameButton>
    {
        public void EnterState(GameButton obj)
        {
            obj.ChangePosition = obj.Button.transform.position;
            obj.CurrentTime = 0;
        }

        public void UpdateState(GameButton obj)
        {
            if (obj.Button.transform.position != obj.InitialPosition)
            {
                obj.CurrentPosition = Vector3.Slerp(obj.ChangePosition, obj.InitialPosition, obj.CurrentTime / obj.PressTime);
                obj.CurrentTime += Time.fixedDeltaTime;
            }
            else
            {
                obj.CurrentPosition = obj.InitialPosition;
                obj.Button.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }

        public void ExitState(GameButton obj)
        {
            
        }
    }
}
