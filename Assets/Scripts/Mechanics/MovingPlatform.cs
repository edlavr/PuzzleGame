using System;
using System.Collections;
using UnityEngine;

namespace Mechanics
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private Transform platform;
        [SerializeField] private Rigidbody rb;
        [Header("Variables")]
        [SerializeField] private float speed;
        [SerializeField] private MovingPlatformMode mode;
        private int _pingPongValue = 1;


        private enum MovingPlatformMode
        {
            Loop,
            PingPong,
            Reset
        }
        
        private void Awake()
        {
            platform.position = points[0].position;
            StartCoroutine(MovePlatform());
        }

        private IEnumerator MovePlatform()
        {
            int _currentPoint = 0;
            while (true)
            {
                platform.position = points[_currentPoint].position;
                _currentPoint = GetNextPoint(_currentPoint);
                //Debug.Log(_currentPoint);
                Vector3 _direction = (points[_currentPoint].position - platform.position).normalized;

                while (Vector3.Magnitude(platform.position - points[_currentPoint].position) > 0.01f)
                {
                    rb.MovePosition(platform.position + _direction * speed);
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        private int GetNextPoint(int point)
        {
            switch (mode)
            {
                case MovingPlatformMode.Loop:
                    return (point + 1) % points.Length;
                
                case MovingPlatformMode.PingPong:
                    if (point + 1 == points.Length && _pingPongValue == 1)
                    {
                        _pingPongValue = -1;
                    }
                    else if (point == 0 && _pingPongValue == -1)
                    {
                        _pingPongValue = 1;
                    }
                    return point + _pingPongValue;
                
                case MovingPlatformMode.Reset when point != points.Length - 1:
                    return point + 1;
                
                case MovingPlatformMode.Reset:
                    platform.position = points[0].position;
                    return 1;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
