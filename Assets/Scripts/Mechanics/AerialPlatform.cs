using Input;
using Interaction;
using Interaction.RecordSM;
using UnityEngine;

namespace Mechanics
{
    public class AerialPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject _destination;
        
        private const float GRAVITY = -9.815f;
        
        private Vector3 _direction;
        private float _distance;

        private float _velocity;
        private float _velocityX;
        private float _velocityY;
        private float _velocityZ;

        [SerializeField] private float angle = 45;
        private float _sin;
        private float _tan;
        public CharacterControl Player;
        private void Awake()
        {
            _sin = Mathf.Sin(angle * Mathf.Deg2Rad);
            _tan = Mathf.Tan(angle * Mathf.Deg2Rad);
            var _transform = transform;
            var _destPosition = _destination.transform.position;
            var _position = _transform.position;
            
            _direction = (_destPosition - _position);
            _distance = Vector3.Distance(_position, _destPosition);
            _velocity = Mathf.Sqrt((2 * -GRAVITY * _tan * _distance) / Mathf.Pow(_sin, 2)) / 2f * _sin;
            
            _velocityX = _velocity * _direction.x / (_tan * _distance);
            _velocityY = _velocity;
            _velocityZ = _velocity * _direction.z / (_tan * _distance);
            
            _direction = new Vector3(_velocityX, _velocityY, _velocityZ);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                if (!other.GetComponent<Recordable>().IsPickedup())
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
