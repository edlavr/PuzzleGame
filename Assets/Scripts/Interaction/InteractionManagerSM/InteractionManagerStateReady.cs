using Interaction.PickupSM;
using Interaction.RecordSM;

namespace Interaction.InteractionManagerSM
{
    public class InteractionManagerStateReady : InteractionManagerStateBase
    {

        public override void UpdateState(InteractionManager interactionManager)
        {
            Interactable = interactionManager.CastCheck();
            if (Interactable != null)
            {
                Pickupable = Interactable.GetComponent<Pickupable>();
                Recordable = Interactable.GetComponent<Recordable>();
            }
            else
            {
                interactionManager.ChangeState(interactionManager.IdleManagerState);
                return;
            }
            
            
            if (interactionManager.IsInteractPressed)
            {
                interactionManager.IsInteractPressed = false;
                interactionManager.ChangeState(interactionManager.ActiveManagerState);
            }
            else if (interactionManager.IsRecordPressed)
            {
                if (Recordable == null) return;

                if (Recordable.CurrentRecordState != Recordable.RecordActiveState)
                {
                    interactionManager.IsRecordPressed = false;
                    interactionManager.RecordedInteractableObjs.Add(Recordable);
                    Recordable.ChangeRecordState(Recordable.RecordActiveState);
                }
            }
            
        }

        public override void ExitState(InteractionManager interactionManager)
        {
            
        }
    }
}
