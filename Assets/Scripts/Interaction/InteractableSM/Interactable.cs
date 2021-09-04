using Cinemachine;
using Interaction.InteractionSM;
using UnityEngine;

namespace Interaction.InteractableSM
{
    public class Interactable : MonoBehaviour
    {
        private InteractableStateBase _currentState;
        public readonly InteractableStateIdle IdleState = new InteractableStateIdle();
        public readonly InteractableStateActive ActiveState = new InteractableStateActive();
        
        // Physics
        public InteractionControl interactionControl;
        public CinemachineVirtualCamera playerCamera;
        public GameObject interactableDest;

        [HideInInspector] public Rigidbody _rb;
        [Header("Variables")]
        public float MinSpeed = 1;
        public float MaxSpeed = 10;
        public float RotSpeed = 10;
        public float MaxDistance = 3f;
        public float BreakDistance = 10f;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _currentState = IdleState;
            _currentState.EnterState(this);
        }

        public void ChangeState()
        {
            _currentState.ExitState(this);
            _currentState = _currentState == IdleState ? (InteractableStateBase) ActiveState : IdleState;
            _currentState.EnterState(this);
        }

        private void FixedUpdate()
        {
            _currentState.UpdateState(this);
        }
    }
}
