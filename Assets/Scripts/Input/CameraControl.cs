using Cinemachine;
using UnityEngine;

namespace Input
{
    public class CameraControl : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;
        
        [Header("Variables")]
        public float mouseSensitivity = 10f;
        
        private float _lookX;
        private float _lookY;

        private float _rotation;

        private void Awake()
        {
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        private void OnEnable()
        {
            InputManager.OnLook += LookHandler;
        }
        
        private void OnDisable()
        {
            InputManager.OnLook -= LookHandler;
        }

        private void LookHandler(Vector2 look)
        {
            _lookX = look.x * Time.deltaTime * mouseSensitivity;
            _lookY = look.y * Time.deltaTime * mouseSensitivity;
        }

        private void Look()
        {
            _rotation -= _lookY;
            _rotation = Mathf.Clamp(_rotation, -45f, 45f);
            _camera.transform.localRotation = Quaternion.Euler(_rotation, 0f, 0f);
            transform.Rotate(Vector3.up * _lookX);
        }

        private void Update()
        {
            Look();
        }
    }

}
