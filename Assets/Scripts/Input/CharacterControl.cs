using UnityEngine;

namespace Input
{
    public class CharacterControl : MonoBehaviour
    {
        private CharacterController _characterController;
        [Header("Ground Check")]
        [SerializeField] private Transform _groundCheckPos;
        [SerializeField] private LayerMask GroundMask;

        [Header("Variables")]
        [SerializeField] private float _speed = 5f; 
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private float _gravityMultiplier = 2f;
        [SerializeField] private float _forceMultiplier = 1f;

        // Walking
        private Vector3 _move;
        private float _moveX;
        private float _moveZ;
        
        // Jumping
        private Vector3 _jumpMove;
        private float gravity = -9.81f;
        private float _jumpVelocity;
        private bool _jumpPressed;
        private bool _isJumping;



        private void Awake()
        {
            JumpVariables();
            _characterController = GetComponent<CharacterController>();
        }

        private void JumpVariables()
        {
            gravity *= _gravityMultiplier;
            _jumpVelocity = Mathf.Sqrt(_jumpHeight * -2f * gravity) * .5f;
        }

        private void OnEnable()
        {
            InputManager.OnMove += MoveHandler;
            InputManager.OnJump += JumpHandler;
        }

        private void OnDisable()
        {
            InputManager.OnMove -= MoveHandler;
            InputManager.OnJump -= JumpHandler;
        }

        private void MoveHandler(Vector2 movement)
        {
            _moveX = movement.x;
            _moveZ = movement.y;
        }
        
        private void JumpHandler()
        {
            _jumpPressed = true;
        }

        private void Walk()
        {
            var _transform = transform;
            _move = _transform.right * (_moveX * _speed) + _transform.forward * (_moveZ * _speed);
            _characterController.Move(_move * Time.deltaTime);
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(_groundCheckPos.position, .5f, GroundMask);
        }

        private void GravityAndJump()
        {
            if (IsGrounded() && _jumpMove.y < 0)
            {
                _isJumping = false;
                _jumpMove.y = -2f;
            }
            
            if (_jumpPressed && IsGrounded())
            {
                _isJumping = true;
                _jumpMove.y = _jumpVelocity;
            }

            if (_isJumping)
            {
                _jumpPressed = false;
            }
            
            _jumpMove.y = (_jumpMove.y * 2 + gravity * Time.deltaTime) * .5f;

            _characterController.Move(_jumpMove * Time.deltaTime);

            _jumpPressed = false;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.layer != 7) return;
            if (_groundCheckPos.position.y - 0.5f > hit.gameObject.transform.position.y)
            {
                return;
            }
            Rigidbody _rigidbody = hit.collider.attachedRigidbody;
            var _position = transform.position;
            
            Vector3 _forceDirection = hit.gameObject.transform.position - _position;
            _forceDirection.y = 0;
            _forceDirection.Normalize();
            _rigidbody.velocity += _forceDirection * _forceMultiplier;
        }
        
        private void Update()
        {
            Walk();
            GravityAndJump();
        }
    }
}
