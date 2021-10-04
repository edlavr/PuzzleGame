namespace Interaction.InteractionSM
{
    public class InteractionStateActive : InteractionStateBase
    {
        public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
            InteractableObject = interactionControl.CastCheck();
            InteractableObject.ChangeInteractionState(InteractableObject.InteractActiveState);
        }

        public override void UpdateState(InteractionControl interactionControl)
        {
            if (interactionControl.IsRecordPressed && InteractableObject.CurrentRecordState != InteractableObject.RecordActiveState)
            {
                interactionControl.IsRecordPressed = false;
                interactionControl.RecordedInteractables.Add(InteractableObject);
                InteractableObject.ChangeRecordState(InteractableObject.RecordActiveState);
            }
            if (interactionControl.IsInteractPressed || interactionControl.InteractionBroken)
            {
                interactionControl.InteractionBroken = false;
                interactionControl.IsInteractPressed = false;
                InteractableObject.ChangeInteractionState(InteractableObject.InteractIdleState);
                InteractableObject = null;
                interactionControl.ChangeState(interactionControl.ReadyState);
            }
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
