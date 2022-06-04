using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interaction.PickupSM
{
    public class Pickupable : Interactable
    {
        [FormerlySerializedAs("interactionControl")] public InteractionManager interactionManager;
        public CinemachineVirtualCamera playerCamera;
        public GameObject interactableDest;
        internal Rigidbody RB;
        [Header("Variables")]
        public float MinSpeed = 100;
        public float MaxSpeed = 4000;
        public float RotSpeed = 500;
        public float MaxDistance = 1.5f;
        public float BreakDistance = 3f;
        
        internal IStateMachine<Pickupable> CurrentPickupState;
        internal readonly IStateMachine<Pickupable> PickupIdleState = new PickupStateIdle();
        internal readonly IStateMachine<Pickupable> PickupActiveState = new PickupStateActive();
        
        // Material
        internal Material Material;
        
        private void Awake()
        {
            RB = GetComponent<Rigidbody>();
            Material = GetComponent<Renderer>().material;
        }

        internal virtual void Start()
        {
            CurrentPickupState = PickupIdleState;
            CurrentPickupState.EnterState(this);
        }
        
        internal virtual void Update()
        {
            CurrentPickupState.UpdateState(this);
        }

        public bool IsPickedup()
        {
            return CurrentPickupState == PickupActiveState;
        }
        
        public void ChangePickupState(IStateMachine<Pickupable> state)
        {
            if (CurrentPickupState == state) return;
            CurrentPickupState.ExitState(this);
            CurrentPickupState = state;
            CurrentPickupState.EnterState(this);
        }
        
        
    }
}
