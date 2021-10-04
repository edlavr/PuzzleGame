namespace Interaction.InteractionSM
{
    public class InteractionStateReady : InteractionStateBase
    {
        public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
            InteractableObject = interactionControl.CastCheck();
        }

        public override void UpdateState(InteractionControl interactionControl)
        {
            bool _castCheck = interactionControl.CastCheck();
            if (!_castCheck)
            {
                interactionControl.ChangeState(interactionControl.IdleState);
                return;
            }
            
            InteractableObject = interactionControl.CastCheck();
            
            if (interactionControl.IsInteractPressed)
            {
                interactionControl.IsInteractPressed = false;
                interactionControl.ChangeState(interactionControl.ActiveState);
            }
            else if (interactionControl.IsRecordPressed && InteractableObject.CurrentRecordState != InteractableObject.RecordActiveState)
            {
                interactionControl.IsRecordPressed = false;
                interactionControl.RecordedInteractables.Add(InteractableObject);
                InteractableObject.ChangeRecordState(InteractableObject.RecordActiveState);
            }
            
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
