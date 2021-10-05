namespace Interaction.InteractionSM
{
    public class InteractionStateReady : InteractionStateBase
    {
        public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
            InteractableObj = interactionControl.CastCheck();
        }

        public override void UpdateState(InteractionControl interactionControl)
        {
            bool _castCheck = interactionControl.CastCheck();
            if (!_castCheck)
            {
                interactionControl.ChangeState(interactionControl.IdleState);
                return;
            }
            
            InteractableObj = interactionControl.CastCheck();
            
            if (interactionControl.IsInteractPressed)
            {
                interactionControl.IsInteractPressed = false;
                interactionControl.ChangeState(interactionControl.ActiveState);
            }
            else if (interactionControl.IsRecordPressed && InteractableObj.CurrentRecordState != InteractableObj.RecordActiveState)
            {
                interactionControl.IsRecordPressed = false;
                interactionControl.RecordedInteractableObjs.Add(InteractableObj);
                InteractableObj.ChangeRecordState(InteractableObj.RecordActiveState);
            }
            
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
