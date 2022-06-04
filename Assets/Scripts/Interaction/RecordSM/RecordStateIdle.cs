using UnityEngine;

namespace Interaction.RecordSM
{
    public class RecordStateIdle : IStateMachine<Recordable>
    {
        public void EnterState(Recordable recordable)
        {
            recordable.Material.shader = Shader.Find("HDRP/Lit");
            recordable.Material.SetColor("_BaseColor", Color.white);
        }

        public void UpdateState(Recordable recordable)
        {
            
        }

        public void ExitState(Recordable recordable)
        {
            
        }
    }
}
