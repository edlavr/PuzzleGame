using System.Collections.Generic;
using Mechanics.GameButtonSM;
using UnityEngine;

namespace Mechanics
{
    public class GameButton : MonoBehaviour
    {
        internal Vector3 InitialPosition;
        internal Vector3 CurrentPosition;
        internal Vector3 PressedPosition;
        internal Vector3 ChangePosition;
        public GameObject Button;
        [Header("Variables")]
        [SerializeField] private float pressHeight = 0.07f;
        [SerializeField] private float pressSpeed = .3f;
        internal float PressTime;
        internal float CurrentTime;
        private readonly List<GameObject> pressedBy = new List<GameObject>();
    
        //
        private IStateMachine<GameButton> currentButtonState;
        private readonly IStateMachine<GameButton> _idleState = new GameButtonStateIdle();
        private readonly IStateMachine<GameButton> _pressedState = new GameButtonStatePressed();
        private void Awake()
        {
            InitialPosition = Button.transform.position;
            CurrentPosition = InitialPosition;
            PressedPosition = InitialPosition - new Vector3(0, pressHeight, 0);
            PressTime = pressHeight / pressSpeed;

            ChangeState(_idleState);
        }

        public void ChangeState(IStateMachine<GameButton> state)
        {
            if (currentButtonState == state) return;
            currentButtonState?.ExitState(this);
            currentButtonState = state;
            currentButtonState.EnterState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            pressedBy.Add(other.gameObject);
            ChangeState(_pressedState);
        }

        private void OnTriggerExit(Collider other)
        {
            pressedBy.Remove(other.gameObject);
            if (pressedBy.Count == 0)
            {
                ChangeState(_idleState);
            }
        }

        private void FixedUpdate()
        {
            currentButtonState.UpdateState(this);
            Button.transform.position = CurrentPosition;

        }
    }
}
