using Fsi.Prototyping.Gameplay;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Gameplay.Gameplay
{
    public class GameplayPlayer : MonoBehaviour
    {
        public ThirdPersonController ThirdPersonController { get; private set; }
        
        [Header("Player")]
        
        [SerializeField]
        private ThirdPersonController thirdPersonControllerPrefab;

        [SerializeField]
        private CinemachineCamera playerCamera;
        
        [Header("Input")]
        
        [SerializeField]
        private InputActionReference movementInputRef;
        
        private InputAction movementInput;

        private Vector2 movementVector;

        private void Awake()
        {
            movementInput = movementInputRef.ToInputAction();
            
            ThirdPersonController = Instantiate(thirdPersonControllerPrefab);
            playerCamera.Target.TrackingTarget = ThirdPersonController.transform;
        }

        private void Update()
        {
            movementVector = movementInput.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            ThirdPersonController.ProvideMovementInput(movementVector);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}
