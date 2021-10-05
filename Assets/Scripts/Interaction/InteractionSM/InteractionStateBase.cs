using System;

namespace Interaction.InteractionSM
{
    public abstract class InteractionStateBase : IStateMachine<InteractionControl>
    {
        public static event Action<InteractionStateBase> OnInteractionStateChanged;
        protected static InteractableObj InteractableObj;

        public virtual void EnterState(InteractionControl interactionControl)
        {
            OnInteractionStateChanged?.Invoke(this);
        }

        public abstract void UpdateState(InteractionControl interactionControl);

        public abstract void ExitState(InteractionControl interactionControl);
    }
}