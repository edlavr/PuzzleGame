using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] private float timeout;
    private Transform _transform;
    private Coroutine _coroutine;

    private void Awake()
    {
        _transform = transform;
    }

    public void DoUnblock()
    {
        _coroutine ??= StartCoroutine(Unblock());
    }
    
    private IEnumerator Unblock()
    {
        _transform.position += Vector3.up * 3;
        yield return new WaitForSeconds(timeout);
        _transform.position -= Vector3.up * 3;
        _coroutine = null;
    }
}
