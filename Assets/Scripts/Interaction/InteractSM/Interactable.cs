using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent Event;
    
    public void Do()
    {
        Event?.Invoke();
    }
}
