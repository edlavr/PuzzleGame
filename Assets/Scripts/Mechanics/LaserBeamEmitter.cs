using System;
using UnityEngine;

namespace Mechanics
{
    public class LaserBeamEmitter : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private void OnEnable()
        {
            EmitBeam();
        }
        
        private void EmitBeam()
        {
            Physics.Raycast(transform.position, Vector3.forward, Mathf.Infinity);
        }
    }
}
