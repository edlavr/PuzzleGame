using System;
using System.Reflection;
using Interaction;
using Interaction.InteractionSM;
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
            InteractionStateBase.OnInteractionStateChanged += ChangeAlpha;
        }
        
        private void OnDisable()
        {
            InteractionStateBase.OnInteractionStateChanged -= ChangeAlpha;
        }

        private void ChangeAlpha(InteractionStateBase interactionStateBase)
        {
            if (interactionStateBase.GetType() == typeof(InteractionStateIdle))
            {
                _crosshairAlpha.alpha = .3f;
            }
            else if (interactionStateBase.GetType() == typeof(InteractionStateReady))
            {
                _crosshairAlpha.alpha = 1f;
            }
            else if (interactionStateBase.GetType() == typeof(InteractionStateActive))
            {
                _crosshairAlpha.alpha = 0f;
            }
        }


    }
}
