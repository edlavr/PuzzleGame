using Interaction.PickupSM;
using UnityEngine;

namespace Interaction.InteractionManagerSM
{
    public class InteractionManagerStateActive : InteractionManagerStateBase
    {
        public override void EnterState(InteractionManager interactionManager)
        {
            base.EnterState(interactionManager);
            
            Interactable.Do();
            if (Pickupable == null && Recordable == null)
            {
                interactionManager.ChangeState(interactionManager.ReadyManagerState);
            }
            
            if (Pickupable == null) return;
            Pickupable.ChangePickupState(Pickupable.PickupActiveState);
        }

        public override void UpdateState(InteractionManager interactionManager)
        {
            if (interactionManager.IsRecordPressed && Recordable != null)
            {
                if (Recordable.CurrentRecordState != Recordable.RecordActiveState)
                {
                    interactionManager.IsRecordPressed = false;
                    interactionManager.RecordedInteractableObjs.Add(Recordable);
                    Recordable.ChangeRecordState(Recordable.RecordActiveState);
                }
            }
            if ((interactionManager.IsInteractPressed || interactionManager.InteractionBroken) && Pickupable != null)
            {
                interactionManager.InteractionBroken = false;
                interactionManager.IsInteractPressed = false;
                Pickupable.ChangePickupState(Pickupable.PickupIdleState);
                Pickupable.gameObject.layer = 7;
                Interactable = null;
                Pickupable = null;
                Recordable = null;
                interactionManager.ChangeState(interactionManager.ReadyManagerState);
            }
        }

        public override void ExitState(InteractionManager interactionManager)
        {
            
        }
    }
}
