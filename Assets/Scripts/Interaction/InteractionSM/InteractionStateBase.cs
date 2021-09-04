using System;
using UnityEngine;

namespace Interaction.InteractionSM
{
    public abstract class InteractionStateBase
    {
        public static event Action<InteractionStateBase> OnInteractionStateChanged;
        public static InteractableSM.Interactable interactable;

        public virtual void EnterState(InteractionControl interactionControl)
        {
            OnInteractionStateChanged?.Invoke(this);
        }

        public abstract void UpdateState(InteractionControl interactionControl);

        public abstract void ExitState(InteractionControl interactionControl);
    }
}