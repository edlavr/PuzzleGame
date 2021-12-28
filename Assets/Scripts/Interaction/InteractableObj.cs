using System;
using System.Collections.Generic;
using Cinemachine;
using Interaction.InteractableObjSM;
using Interaction.InteractSM;
using Interaction.RecordSM;
using UnityEngine;

namespace Interaction
{
    public class InteractableObj : MonoBehaviour
    {
        internal IStateMachine<InteractableObj> CurrentInteractState;
        internal readonly IStateMachine<InteractableObj> InteractIdleState = new InteractObjStateIdle();
        internal readonly IStateMachine<InteractableObj> InteractActiveState = new InteractObjStateActive();

        internal IStateMachine<InteractableObj> CurrentRecordState;
        internal readonly IStateMachine<InteractableObj> RecordIdleState = new RecordStateIdle();
        internal readonly IStateMachine<InteractableObj> RecordActiveState = new RecordStateActive();
        internal readonly IStateMachine<InteractableObj> RecordRewindState = new RecordStateRewind();

        // Physics
        public InteractionControl interactionControl;
        public CinemachineVirtualCamera playerCamera;
        public GameObject interactableDest;
        internal Rigidbody RB;
        [Header("Variables")]
        public float MinSpeed = 100;
        public float MaxSpeed = 4000;
        public float RotSpeed = 500;
        public float MaxDistance = 1.5f;
        public float BreakDistance = 3f;

        // Rewind
        internal readonly List<PointInTime> PointsInTime = new List<PointInTime>();
        internal bool IsRewinding;
        
        // Material
        internal Material Material;
        public float ColorSpeed = .5f;
        //internal Color BaseColor;
        //internal Color PaintColor;
        private static readonly int BaseColorID = Shader.PropertyToID("BaseColor");
        private static readonly int PaintColorID = Shader.PropertyToID("PaintColor");

        public bool IsInteractActive()
        {
            return CurrentInteractState == InteractActiveState;
        }

        private void Awake()
        {
            RB = GetComponent<Rigidbody>();
            Material = GetComponent<Renderer>().material;
            //BaseColor = Material.GetColor(BaseColorID);
            //PaintColor = Material.GetColor(PaintColorID);
        }

        private void Start()
        {
            CurrentInteractState = InteractIdleState;
            CurrentInteractState.EnterState(this);
            CurrentRecordState = RecordIdleState;
            CurrentRecordState.EnterState(this);
        }

        public void ChangeInteractionState(IStateMachine<InteractableObj> state)
        {
            if (CurrentInteractState == state) return;
            if (state == RecordIdleState || state == RecordActiveState)
            {
                throw new ArgumentException("Interaction state cannot be a record state");
            }
            CurrentInteractState.ExitState(this);
            CurrentInteractState = state;
            CurrentInteractState.EnterState(this);
        }

        public void ChangeRecordState(IStateMachine<InteractableObj> state)
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

        private void Update()
        {
            CurrentRecordState.UpdateState(this);
            CurrentInteractState.UpdateState(this);
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
