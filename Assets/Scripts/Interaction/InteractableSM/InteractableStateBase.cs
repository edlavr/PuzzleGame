using UnityEngine;

namespace Interaction.InteractableSM
{
    public abstract class InteractableStateBase
    {
        public abstract void EnterState(Interactable interactable);
        
        public abstract void UpdateState(Interactable interactable);
        
        public abstract void ExitState(Interactable interactable);
    }
}
