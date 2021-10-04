using System;
using System.Collections.Generic;
using Cinemachine;
using Interaction.InteractSM;
using Interaction.RecordSM;
using UnityEngine;

namespace Interaction
{
    public class Interactable : MonoBehaviour
    {
        private IStateMachine<Interactable> _currentInteractState;
        public readonly IStateMachine<Interactable> InteractIdleState = new InteractObjStateIdle();
        public readonly IStateMachine<Interactable> InteractActiveState = new InteractObjStateActive();

        public IStateMachine<Interactable> CurrentRecordState;
        public readonly IStateMachine<Interactable> RecordIdleState = new RecordStateIdle();
        public readonly IStateMachine<Interactable> RecordActiveState = new RecordStateActive();
        public readonly IStateMachine<Interactable> RecordRewindState = new RecordStateRewind();

        // Physics
        public InteractionControl interactionControl;
        public CinemachineVirtualCamera playerCamera;
        public GameObject interactableDest;
        [HideInInspector] public Rigidbody RB;
        [HideInInspector] public BoxCollider BC;
        [Header("Variables")]
        public float MinSpeed = 1;
        public float MaxSpeed = 10;
        public float RotSpeed = 10;
        public float MaxDistance = 3f;
        public float BreakDistance = 10f;

        // Rewind
        public List<PointInTime> PointsInTime = new List<PointInTime>();
        [HideInInspector] public bool isRewinding;
        
        // Material
        [HideInInspector] public Material Material;
        public float ColorSpeed = .5f;
        [HideInInspector] public Color BaseColor;
        [HideInInspector] public Color PaintColor;
        

        private void Awake()
        {
            RB = GetComponent<Rigidbody>();
            BC = GetComponent<BoxCollider>();
            Material = GetComponent<Renderer>().material;
            BaseColor = Material.GetColor("BaseColor");
            PaintColor = Material.GetColor("PaintColor");
        }

        private void Start()
        {
            _currentInteractState = InteractIdleState;
            _currentInteractState.EnterState(this);
            CurrentRecordState = RecordIdleState;
            CurrentRecordState.EnterState(this);
        }

        public void ChangeInteractionState(IStateMachine<Interactable> state)
        {
            if (_currentInteractState == state) return;
            if (state == RecordIdleState || state == RecordActiveState)
            {
                throw new ArgumentException("Interaction state cannot be a record state");
            }
            _currentInteractState.ExitState(this);
            _currentInteractState = state;
            _currentInteractState.EnterState(this);
        }

        public void ChangeRecordState(IStateMachine<Interactable> state)
        {
            if (CurrentRecordState == state) return;
            if (state == InteractIdleState || state == InteractActiveState)
            {
                throw new ArgumentException("Record state cannot be an interactable state");
            }
            CurrentRecordState.ExitState(this);
            CurrentRecordState = state;
            CurrentRecordState.EnterState(this);
        }

        private void FixedUpdate()
        {
            _currentInteractState.UpdateState(this);
        }

        private void Update()
        {
            CurrentRecordState.UpdateState(this);
        }

        public void RewindOrInterrupt()
        {
            if (CurrentRecordState == RecordRewindState)
            {
                ChangeRecordState(RecordIdleState);
            }
            else
            {
                ChangeRecordState(RecordRewindState);
            }
        }

    }
}
