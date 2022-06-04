using System;
using System.Collections;
using UnityEngine;

namespace Input
{
    public class CharacterControl : MonoBehaviour
    {
        private Rigidbody _RB;
        [Header("Ground Check")]
        [SerializeField] private Transform _groundCheckPos;
        [SerializeField] private LayerMask GroundMask;
        [SerializeField] private LayerMask PlatformMask;

        [Header("Variables")]
        [SerializeField] private float _speed = 5f; 
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private float _gravityMultiplier = 2f;

        // Walking
        [HideInInspector] public Vector3 _move;
        private float _moveX;
        private float _moveZ;
        
        // Jumping
        private float gravity = -9.81f;
        private float _jumpVelocity;

        private bool launch;

        private void Awake()
        {
            JumpVariables();
            _RB = GetComponent<Rigidbody>();
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
            GravityAndJump();
        }

        private void Walk()
        {
            if (launch)
            {
                return;
            }
            var _transform = transform;
            if ((_moveX != 0f || _moveZ != 0f) && !launch)
            {
                _move = _transform.right * (_moveX * _speed) + _transform.forward * (_moveZ * _speed);
                _RB.velocity = new Vector3(_move.x, _RB.velocity.y, _move.z);
            }
            else if (_moveX == 0f && _moveZ == 0f && !launch)
            {
                _RB.velocity = new Vector3(0, _RB.velocity.y, 0);
            }
            else if ((_moveX != 0f || _moveZ != 0f) && launch)
            {
                _move = _transform.right * (_moveX * _speed) + _transform.forward * (_moveZ * _speed);
                _RB.velocity += new Vector3(_move.x / 100, 0, _move.z / 100);
            }

            _RB.velocity += PlatformVelocity();
        }

        public void Launch(Vector3 direction)
        {
            launch = true;
            _RB.velocity = Vector3.zero;
            _RB.angularVelocity = Vector3.zero;
            _RB.velocity = direction;
            StartCoroutine(UpdateLaunchBool());
        }

        private IEnumerator UpdateLaunchBool()
        {
            yield return new WaitForSeconds(0.3f);
            while (!IsGrounded())
            {
                yield return null;
            }
            launch = false;
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(_groundCheckPos.position, .5f, GroundMask);
        }
        
        private Vector3 PlatformVelocity()
        {
            if (Physics.Raycast(_groundCheckPos.position, Vector3.down, out var _hit, 1.5f, PlatformMask))
            {
                return _hit.collider.GetComponent<Rigidbody>().velocity;
            }

            return Vector3.zero;
        }

        private void GravityAndJump()
        {
            if (IsGrounded())
            {
                _RB.AddForce(new Vector3(0, _jumpVelocity, 0), ForceMode.VelocityChange);
            }
        }
        
        // push boxes
        /*private void OnControllerColliderHit(ControllerColliderHit hit)
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
        }*/
        
        private void Update()
        {
            Walk();
        }
    }
}
