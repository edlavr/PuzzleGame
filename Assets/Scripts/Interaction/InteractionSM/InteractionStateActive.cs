namespace Interaction.InteractionSM
{
    public class InteractionStateActive : InteractionStateBase
    {
        public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
            InteractableObj = interactionControl.CastCheck();
            InteractableObj.ChangeInteractionState(InteractableObj.InteractActiveState);
        }

        public override void UpdateState(InteractionControl interactionControl)
        {
            if (interactionControl.IsRecordPressed && InteractableObj.CurrentRecordState != InteractableObj.RecordActiveState)
            {
                interactionControl.IsRecordPressed = false;
                interactionControl.RecordedInteractableObjs.Add(InteractableObj);
                InteractableObj.ChangeRecordState(InteractableObj.RecordActiveState);
            }
            if (interactionControl.IsInteractPressed || interactionControl.InteractionBroken)
            {
                interactionControl.InteractionBroken = false;
                interactionControl.IsInteractPressed = false;
                InteractableObj.ChangeInteractionState(InteractableObj.InteractIdleState);
                InteractableObj.gameObject.layer = 7;
                InteractableObj = null;
                interactionControl.ChangeState(interactionControl.ReadyState);
            }
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
