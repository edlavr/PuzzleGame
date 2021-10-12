using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class GameButton : MonoBehaviour
    {
        private Vector3 _initialY;
        private Vector3 _pressedY;
        private GameObject _button;
        [Header("Variables")]
        [SerializeField] private float pressHeight = 0.1f;
        [SerializeField] private float pressTime = 1f;

        private IEnumerator _buttonCoroutine;
    
        private readonly List<GameObject> pressedBy = new List<GameObject>();

        private void Awake()
        {
            _button = transform.GetChild(0).gameObject;
            _initialY = _button.transform.position;
            _pressedY = _initialY - new Vector3(0, pressHeight, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            pressedBy.Add(other.gameObject);
            if (_buttonCoroutine != null)
            {
                StopCoroutine(_buttonCoroutine);
            }
            _buttonCoroutine = PressButton();
            StartCoroutine(_buttonCoroutine);
        }

        private void OnTriggerExit(Collider other)
        {
            pressedBy.Remove(other.gameObject);
            if (pressedBy.Count == 0)
            {
                StopCoroutine(_buttonCoroutine);
                _buttonCoroutine = ReleaseButton();
                StartCoroutine(_buttonCoroutine);
            }
        }

        private IEnumerator PressButton()
        {
            Debug.Log("press");
            _button.GetComponent<MeshRenderer>().material.color = Color.blue;
            float _currentTime = 0;
            while (_button.transform.position.y > _pressedY.y)
            {
                _button.transform.position = Vector3.Slerp(_button.transform.position, _pressedY, _currentTime / pressTime);
                _currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        
        }
    
        private IEnumerator ReleaseButton()
        {
            Debug.Log("release");
            _button.GetComponent<MeshRenderer>().material.color = Color.red;
            float _currentTime = 0;
            while (_button.transform.position.y < _initialY.y)
            {
                _button.transform.position = Vector3.Slerp(_button.transform.position, _initialY, _currentTime / pressTime);
                _currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
