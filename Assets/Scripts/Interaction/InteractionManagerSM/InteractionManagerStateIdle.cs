namespace Interaction.InteractionManagerSM
{
    public class InteractionManagerStateIdle : InteractionManagerStateBase
    {

        public override void UpdateState(InteractionManager interactionManager)
        {
            Interactable = null;
            interactionManager.IsInteractPressed = false;
            interactionManager.IsRecordPressed = false;
            if (interactionManager.CastCheck())
            {
                interactionManager.ChangeState(interactionManager.ReadyManagerState);
            }
        }

        public override void ExitState(InteractionManager interactionManager)
        {
            
        }
    }
}
