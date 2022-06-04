using System.Collections.Generic;
using Interaction.PickupSM;

namespace Interaction.RecordSM
{
    public class Recordable : Pickupable
    {

        internal IStateMachine<Recordable> CurrentRecordState;
        internal readonly IStateMachine<Recordable> RecordIdleState = new RecordStateIdle();
        internal readonly IStateMachine<Recordable> RecordActiveState = new RecordStateActive();
        internal readonly IStateMachine<Recordable> RecordRewindState = new RecordStateRewind();

        // Rewind
        internal readonly List<PointInTime> PointsInTime = new List<PointInTime>();
        internal bool IsRewinding;
        
        
        //public float ColorSpeed = .5f;
        //internal Color BaseColor;
        //internal Color PaintColor;
        //private static readonly int BaseColorID = Shader.PropertyToID("BaseColor");
        //private static readonly int PaintColorID = Shader.PropertyToID("PaintColor");

        internal override void Start()
        {
            base.Start();
            CurrentRecordState = RecordIdleState;
            CurrentRecordState.EnterState(this);
        }

        public void ChangeRecordState(IStateMachine<Recordable> state)
        {
            if (CurrentRecordState == state) return;
            CurrentRecordState.ExitState(this);
            CurrentRecordState = state;
            CurrentRecordState.EnterState(this);
        }

        internal override void Update()
        {
            base.Update();
            CurrentRecordState.UpdateState(this);
        }

        public void RewindOrInterrupt()
        {
            ChangeRecordState(CurrentRecordState == RecordRewindState ? RecordIdleState : RecordRewindState);
        }

    }
}
