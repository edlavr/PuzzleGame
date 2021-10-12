using Input;
using Interaction;
using UnityEngine;

namespace Mechanics
{
    public class AerialPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject _destination;
        
        private Vector3 _direction;
        private float _distance;
        private float gravity = -9.815f;
        
        private float _velocity;
        private float _velocityX;
        private float _velocityY;
        private float _velocityZ;

        public float angle = 45;
        private float sin;
        private float tan;
        public CharacterControl Player;
        private void Awake()
        {
            sin = Mathf.Sin(angle * Mathf.Deg2Rad);
            tan = Mathf.Tan(angle * Mathf.Deg2Rad);
            var _transform = transform;
            var _destPosition = _destination.transform.position;
            var _position = _transform.position;
            
            _direction = (_destPosition - _position);
            _distance = Vector3.Distance(_position, _destPosition);
            _velocity = Mathf.Sqrt((2 * -gravity * tan * _distance) / Mathf.Pow(sin, 2)) / 2f * sin;
            
            _velocityX = _velocity * _direction.x / (tan * _distance);
            _velocityY = _velocity;
            _velocityZ = _velocity * _direction.z / (tan * _distance);
            
            _direction = new Vector3(_velocityX, _velocityY, _velocityZ);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                if (!other.GetComponent<InteractableObj>().IsInteractActive())
                {
                    Rigidbody _rb = other.GetComponent<Rigidbody>();
                    _rb.velocity = Vector3.zero;
                    _rb.angularVelocity = Vector3.zero;
                    _rb.AddForce(_direction, ForceMode.VelocityChange);
                }
            }

            else if (other.gameObject.transform.parent.CompareTag("Player"))
            {
                Player.Launch(_direction);
            }
        }

    }
}
