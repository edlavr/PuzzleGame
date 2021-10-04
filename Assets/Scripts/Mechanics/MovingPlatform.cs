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
        private int prevPoint;
        private int currentPoint = 1;
        private int multiplier = 1;

        private float time;
        private float timerem;
        

        private enum MovingPlatformMode
        {
            Loop,
            PingPong,
            Reset
        }
        
        private void Awake()
        {
            platform.position = points[0].position;
            time = Vector3.Distance(platform.position, points[currentPoint].position) / speed;
        }
        
        private void PingPong(int min, int max)
        {
            if (currentPoint + 1 == max && multiplier == 1)
            {
                multiplier = -1;
            }
            else if (currentPoint == min && multiplier == -1)
            {
                multiplier = 1;
            }
            currentPoint += multiplier;
        }

        private void FixedUpdate()
        {
            if (timerem < time)
            {
                Vector3 _currentPos = Vector3.Lerp(points[prevPoint].position, points[currentPoint].position, timerem/time);
                timerem += Time.fixedDeltaTime;
                rb.MovePosition(_currentPos);
            }
            else
            {
                timerem = 0;
                platform.position = points[currentPoint].position;
                prevPoint = currentPoint;
                switch (mode)
                {
                    case MovingPlatformMode.Loop:
                        currentPoint = (currentPoint + 1) % points.Length;
                        break;
                    case MovingPlatformMode.PingPong:
                        PingPong(0, points.Length);
                        break;
                    case MovingPlatformMode.Reset when currentPoint != points.Length - 1:
                        currentPoint++;
                        break;
                    case MovingPlatformMode.Reset:
                        platform.position = points[0].position;
                        currentPoint = 1;
                        break;
                }
                time = Vector3.Distance(platform.position, points[currentPoint].position) / speed;
            }
        }
    }
}
