namespace Interaction.InteractionSM
{
    public class InteractionStateActive : InteractionStateBase
    {
        public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
            interactable = interactionControl.CastCheck();
            interactable.ChangeState();
        }

        public override void UpdateState(InteractionControl interactionControl)
        {
            if (interactionControl._isInteractPressed || interactionControl._interactionBroken)
            {
                interactionControl._interactionBroken = false;
                interactionControl._isInteractPressed = false;
                interactable.ChangeState();
                interactable = null;
                interactionControl.ChangeState(interactionControl.ReadyState);
            }
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
