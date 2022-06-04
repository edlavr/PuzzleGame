using System;
using Interaction.PickupSM;
using Interaction.RecordSM;

namespace Interaction.InteractionManagerSM
{
    public abstract class InteractionManagerStateBase : IStateMachine<InteractionManager>
    {
        public static event Action<InteractionManagerStateBase> OnInteractionStateChanged;
        protected static Interactable Interactable;
        protected static Pickupable Pickupable;
        protected static Recordable Recordable;

        public virtual void EnterState(InteractionManager interactionManager)
        {
            OnInteractionStateChanged?.Invoke(this);
        }

        public abstract void UpdateState(InteractionManager interactionManager);

        public abstract void ExitState(InteractionManager interactionManager);
    }
}