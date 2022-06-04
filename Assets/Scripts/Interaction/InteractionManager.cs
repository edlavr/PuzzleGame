using System.Collections.Generic;
using Input;
using Interaction.InteractionManagerSM;
using Interaction.RecordSM;
using UnityEngine;

namespace Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        internal bool IsInteractPressed;
        internal bool InteractionBroken;
        
        internal bool IsRecordPressed;
        internal bool IsRewindPressed;

        internal readonly HashSet<Recordable> RecordedInteractableObjs = new HashSet<Recordable>();

        private Camera _mainCamera;
        [SerializeField] private LayerMask _interactableMask;

        [Header("Variables")]
        [SerializeField] private float _castDistance = 10f;

        private RaycastHit _castHit;
        private Vector3 _raycastPos;
        private bool _isHit;
    
        // Interaction State Machine
        private InteractionManagerStateBase _currentManagerState;
        internal readonly InteractionManagerStateIdle IdleManagerState = new InteractionManagerStateIdle();
        internal readonly InteractionManagerStateReady ReadyManagerState = new InteractionManagerStateReady();
        internal readonly InteractionManagerStateActive ActiveManagerState = new InteractionManagerStateActive();

        private void Start()
        {
            ChangeState(IdleManagerState);
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

        public void ChangeState(InteractionManagerStateBase managerState)
        {
            _currentManagerState?.ExitState(this);
            _currentManagerState = managerState;
            _currentManagerState.EnterState(this);
        }

        private void Update()
        {
            _currentManagerState.UpdateState(this);

            if (IsRewindPressed)
            {
                IsRewindPressed = false;
                foreach (var _obj in RecordedInteractableObjs)
                {
                    _obj.RewindOrInterrupt();
                    ChangeState(IdleManagerState);
                }
            }
        }

        public Interactable CastCheck()
        {
            _raycastPos = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            _isHit = Physics.Raycast(_raycastPos, _mainCamera.transform.forward, out _castHit, _castDistance, _interactableMask);
            if (!_isHit) return null;
            var _gameobject = _castHit.transform.gameObject;
            var _interactable = _gameobject.GetComponent<Interactable>();
            var _recordable = _gameobject.GetComponent<Recordable>();
            if (_recordable == null)
            {
                return _interactable;
            }
            return _recordable.IsRewinding ? null : _interactable;
        }
    }
}
