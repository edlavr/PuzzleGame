using System.Collections.Generic;
using Input;
using Interaction.InteractionSM;
using UnityEngine;

namespace Interaction
{
    public class InteractionControl : MonoBehaviour
    {
        [HideInInspector] public bool IsInteractPressed;
        [HideInInspector] public bool InteractionBroken;
        
        [HideInInspector] public bool IsRecordPressed;
        [HideInInspector] public bool IsRewindPressed;

        public HashSet<Interactable> RecordedInteractables = new HashSet<Interactable>();

        private Camera _mainCamera;
        [SerializeField] private LayerMask _interactableMask;

        [Header("Variables")]
        [SerializeField] private float _castRadius = 1f;
        [SerializeField] private float _castDistance = 10f;

        private RaycastHit _castHit;
        private Vector3 _raycastPos;
        private bool _isHit;
    
        // Interaction State Machine
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
            InputManager.OnRecord += RecordHandler;
            InputManager.OnRewind += RewindHandler;
        }
        
        private void OnDisable()
        {
            InputManager.OnInteraction -= InteractionHandler;
            InputManager.OnRecord -= RecordHandler;
            InputManager.OnRewind -= RewindHandler;
        }
    
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void InteractionHandler()
        {
            IsInteractPressed = true;
        }

        private void RecordHandler()
        {
            IsRecordPressed = true;
        }
        
        private void RewindHandler()
        {
            IsRewindPressed = true;
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

            if (IsRewindPressed)
            {
                IsRewindPressed = false;
                foreach (var _obj in RecordedInteractables)
                {
                    _obj.RewindOrInterrupt();
                    ChangeState(IdleState);
                }
            }
        }

        public Interactable CastCheck()
        {
            _raycastPos = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            _isHit = Physics.SphereCast(_raycastPos, _castRadius, _mainCamera.transform.forward, out _castHit, _castDistance, _interactableMask);
            if (!_isHit) return null;
            var _interactable = _castHit.transform.gameObject.GetComponent<Interactable>();
            return !_interactable.isRewinding ? _interactable : null;
        }
    }
}
