using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    internal Vector3 InitialY;
    internal Vector3 PressedY;
    public GameObject Button;
    [Header("Variables")]
    [SerializeField] private float pressHeight = 0.1f;
    [SerializeField] private float pressTime = 1f;
    private float _time;
    private Vector3 _tick;

    private IEnumerator _button;
    
    private readonly List<GameObject> pressedBy = new List<GameObject>();

    private void Awake()
    {
        InitialY = Button.transform.position;
        PressedY = InitialY - new Vector3(0, pressHeight, 0);
        _time = pressHeight / pressTime;
        _tick = new Vector3(0, pressHeight / _time, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        pressedBy.Add(other.gameObject);
        if (_button != null)
        {
            StopCoroutine(_button);
        }
        _button = PressButton();
        StartCoroutine(_button);
    }

    private void OnTriggerExit(Collider other)
    {
        pressedBy.Remove(other.gameObject);
        if (pressedBy.Count == 0)
        {
            StopCoroutine(_button);
            _button = ReleaseButton();
            StartCoroutine(_button);
        }
    }

    private IEnumerator PressButton()
    {
        Debug.Log("press");
        Button.GetComponent<MeshRenderer>().material.color = Color.blue;
        float _currentTime = 0;
        while (Button.transform.position.y > PressedY.y)
        {
            Button.transform.position = Vector3.Slerp(Button.transform.position, PressedY, _currentTime / pressTime);
            _currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
    }
    
    private IEnumerator ReleaseButton()
    {
        Debug.Log("release");
        Button.GetComponent<MeshRenderer>().material.color = Color.red;
        float _currentTime = 0;
        while (Button.transform.position.y < InitialY.y)
        {
            Button.transform.position = Vector3.Slerp(Button.transform.position, InitialY, _currentTime / pressTime);
            _currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
