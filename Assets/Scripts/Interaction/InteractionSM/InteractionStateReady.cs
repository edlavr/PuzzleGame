namespace Interaction.InteractionSM
{
    public class InteractionStateReady : InteractionStateBase
    {
        public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
        }

        public override void UpdateState(InteractionControl interactionControl)
        {
            if (interactionControl.CastCheck() && interactionControl._isInteractPressed)
            {
                interactionControl._isInteractPressed = false;
                interactionControl.ChangeState(interactionControl.ActiveState);
            }
            else if (!interactionControl.CastCheck())
            {
                interactionControl.ChangeState(interactionControl.IdleState);
            }
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
