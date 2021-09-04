using Cinemachine;
using Interaction.InteractableSM;
using Interaction.InteractionSM;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Interactable _interactable = (Interactable) target;
            if(_interactable)
            {
                _interactable.gameObject.layer = LayerMask.NameToLayer("Interactable");
                _interactable.interactableDest = GameObject.Find("INTERACTABLE_DEST");
                _interactable.playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
                _interactable.interactionControl = FindObjectOfType<InteractionControl>();
            }
        }
    }
}
