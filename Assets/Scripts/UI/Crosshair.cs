using System;
using System.Reflection;
using Interaction;
using Interaction.InteractionManagerSM;
using UnityEngine;

namespace UI
{
    public class Crosshair : MonoBehaviour
    {
        private CanvasGroup _crosshairAlpha;

        private void Awake()
        {
            _crosshairAlpha = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            InteractionManagerStateBase.OnInteractionStateChanged += ChangeAlpha;
        }
        
        private void OnDisable()
        {
            InteractionManagerStateBase.OnInteractionStateChanged -= ChangeAlpha;
        }

        private void ChangeAlpha(InteractionManagerStateBase interactionManagerStateBase)
        {
            if (interactionManagerStateBase.GetType() == typeof(InteractionManagerStateIdle))
            {
                _crosshairAlpha.alpha = .3f;
            }
            else if (interactionManagerStateBase.GetType() == typeof(InteractionManagerStateReady))
            {
                _crosshairAlpha.alpha = 1f;
            }
            else if (interactionManagerStateBase.GetType() == typeof(InteractionManagerStateActive))
            {
                _crosshairAlpha.alpha = 0f;
            }
        }


    }
}
