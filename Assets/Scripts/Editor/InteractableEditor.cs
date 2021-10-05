using Cinemachine;
using Interaction;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(InteractableObj))]
    public class InteractableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            InteractableObj _interactableObj = (InteractableObj) target;
            if(_interactableObj)
            {
                _interactableObj.gameObject.layer = LayerMask.NameToLayer("Interactable");
                _interactableObj.interactableDest = GameObject.Find("INTERACTABLE_DEST");
                _interactableObj.playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
                _interactableObj.interactionControl = FindObjectOfType<InteractionControl>();
            }
        }
    }
}
