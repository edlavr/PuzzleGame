using Cinemachine;
using Interaction;
using Interaction.RecordSM;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Recordable))]
    public class InteractableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Recordable _recordable = (Recordable) target;
            if(_recordable)
            {
                //_interactableObj.gameObject.layer = LayerMask.NameToLayer("Interactable");
                _recordable.interactableDest = GameObject.Find("INTERACTABLE_DEST");
                _recordable.playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
                _recordable.interactionManager = FindObjectOfType<InteractionManager>();
            }
        }
    }
}
