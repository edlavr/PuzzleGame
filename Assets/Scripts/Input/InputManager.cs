using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput _playerInput;
        public static event Action<Vector2> OnMove;
        public static event Action OnJump;
        public static event Action<Vector2> OnLook;
        public static event Action OnInteraction;
        public static event Action OnRecord;
        public static event Action OnRewind;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            
            _playerInput.CharacterControl.Jump.started += OnJumpInput;

            _playerInput.CharacterControl.Move.started += OnMoveInput;
            _playerInput.CharacterControl.Move.performed += OnMoveInput;
            _playerInput.CharacterControl.Move.canceled += OnMoveInput;
            
            _playerInput.CharacterControl.Look.started += OnLookInput;
            _playerInput.CharacterControl.Look.performed += OnLookInput;
            _playerInput.CharacterControl.Look.canceled += OnLookInput;

            _playerInput.CharacterControl.Interact.started += OnInteractionInput;
            
            _playerInput.CharacterControl.Record.started += OnRecordInput;
            
            _playerInput.CharacterControl.Rewind.started += OnRewindInput;
        }
        
        private void OnEnable()
        {
            _playerInput.CharacterControl.Enable();
        }

        private void OnDisable()
        {
            _playerInput.CharacterControl.Disable();
        }

        private static void OnMoveInput(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }
        
        private static void OnJumpInput(InputAction.CallbackContext context)
        {
            OnJump?.Invoke();
        }
        
        private static void OnLookInput(InputAction.CallbackContext context)
        {
            OnLook?.Invoke(context.ReadValue<Vector2>());
        }

        private static void OnInteractionInput(InputAction.CallbackContext context)
        {
            OnInteraction?.Invoke();
        }

        private static void OnRecordInput(InputAction.CallbackContext context)
        {
            Debug.Log("record");
            OnRecord?.Invoke();
        }
        
        private static void OnRewindInput(InputAction.CallbackContext context)
        {
            Debug.Log("rewind");
            OnRewind?.Invoke();
        }

    }
}

