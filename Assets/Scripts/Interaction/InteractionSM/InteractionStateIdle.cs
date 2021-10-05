namespace Interaction.InteractionSM
{
    public class InteractionStateIdle : InteractionStateBase
    {
        /*public override void EnterState(InteractionControl interactionControl)
        {
            base.EnterState(interactionControl);
        }*/

        public override void UpdateState(InteractionControl interactionControl)
        {
            InteractableObj = null;
            interactionControl.IsInteractPressed = false;
            interactionControl.IsRecordPressed = false;
            if (interactionControl.CastCheck())
            {
                interactionControl.ChangeState(interactionControl.ReadyState);
            }
        }

        public override void ExitState(InteractionControl interactionControl)
        {
            
        }
    }
}
