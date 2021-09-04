using Input;
using Interaction.InteractableSM;
using UnityEngine;

namespace Interaction.InteractionSM
{
    public class InteractionControl : MonoBehaviour
    {
        public bool _isInteractPressed;
        public bool _interactionBroken;
        private Camera _mainCamera;
        [SerializeField] private LayerMask _interactableMask;

        [Header("Variables")]
        [SerializeField] private float _castRadius = 1f;
        [SerializeField] private float _castDistance = 10f;

        private RaycastHit _castHit;
        private Vector3 _raycastPos;
        private bool _isHit;
    
        // State Machine
        private InteractionStateBase _currentState;
        public readonly InteractionStateIdle IdleState = new InteractionStateIdle();
        public readonly InteractionStateReady ReadyState = new InteractionStateReady();
        public readonly InteractionStateActive ActiveState = new InteractionStateActive();

        private void Start()
        {
            ChangeState(IdleState);
        }
    
        private void OnEnable()
        {
            InputManager.OnInteraction += InteractionHandler;
        }
        
        private void OnDisable()
        {
            InputManager.OnInteraction -= InteractionHandler;
        }
    
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void InteractionHandler()
        {
            _isInteractPressed = true;
        }

        public void ChangeState(InteractionStateBase state)
        {
            _currentState?.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public Interactable CastCheck()
        {
            _raycastPos = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            _isHit = Physics.SphereCast(_raycastPos, _castRadius, _mainCamera.transform.forward, out _castHit, _castDistance, _interactableMask);
            return _isHit ? _castHit.transform.gameObject.GetComponent<Interactable>() : null;
        }
    }
}
