using Fsi.Gameplay.Extensions;
using Fsi.Gameplay.Visuals;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Gameplay.Gameplay.ThirdPerson
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(GroundCheck))]
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Camera")]
        
        [SerializeField]
        private new Camera camera;
        
        [Header("Visuals")]

        [SerializeField]
        private CharacterVisuals visuals;
        protected CharacterVisuals Visuals => visuals;

        [Header("Physics")]
        [SerializeField]
        private bool hasGravity = true;
        
        [SerializeField]
        private float gravity = -9.81f;
        
        [SerializeField]
        private CharacterController characterController;
        public CharacterController CharacterController => characterController;

        [Header("Movement")]

        [SerializeField]
        private MovementProperties groundedMovement;

        [SerializeField]
        private MovementProperties inAirMovement;
        
        [SerializeField]
        private Vector3 velocity;

        [SerializeField]
        private LayerMask moveCheckMask;

        [Header("Jump")]

        [SerializeField]
        private float jumpVelocity = 250f;
        
        [Header("Input")]
        
        [SerializeField]
        private InputActionReference moveActionRef;
        private InputAction moveAction;
        
        [SerializeField]
        private InputActionReference jumpActionRef;
        private InputAction jumpAction;
        
        private Vector2 movementInput;
        
        [Header("Debugging")]

        [Header("Movement")]

        [SerializeField]
        private bool debugWaveMovement;

        protected virtual void Awake()
        {
            moveAction = moveActionRef?.action;
            jumpAction = jumpActionRef?.action;

            if (jumpAction != null)
            {
                jumpAction.performed += OnJumpAction;
            }
        }

        private void OnEnable()
        {
            moveAction?.Enable();
            jumpAction?.Enable();
        }

        private void OnDisable()
        {
            moveAction?.Disable();
            jumpAction?.Disable();
        }

        protected virtual void Update()
        {
            if (!debugWaveMovement)
            {
                movementInput = moveAction.ReadValue<Vector2>();
            }
            else
            {
                float xWave = Mathf.Sin(Time.time);
                float yWave = Mathf.Cos(Time.time);
                movementInput = new Vector2(xWave, yWave);
            }
            
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            ProvideMovementInput(movementInput);
            UpdateMovement();
            UpdateRotation();

            UpdateGravity();
        }

        #region Movement
        
        protected virtual void UpdateMovement()
        {
            Vector3 move = velocity * Time.deltaTime;
            
            characterController.Move(move);
            if (visuals)
            {
                visuals.SetMovement(velocity, false);
            }
        }

        protected virtual void ChangeMovement(Vector3 target)
        {
            target.y = 0;
            target = target.normalized * target.magnitude;
            MovementProperties properties = characterController.isGrounded ? groundedMovement : inAirMovement;

            var flat = velocity;
            flat.y = 0f;
            flat = flat.magnitude * flat.normalized;
            
            var testVel = flat.sqrMagnitude >= target.sqrMagnitude 
                              ? Vector3.MoveTowards(flat, target, properties.Accel * Time.deltaTime) 
                              : Vector3.MoveTowards(flat, target, properties.Deccel * Time.deltaTime);
            testVel.y = 0;
            velocity.x = testVel.x;
            velocity.z = testVel.z;

        }

        protected virtual void ProvideMovementInput(Vector2 input)
        {
            MovementProperties properties = characterController.isGrounded ? groundedMovement : inAirMovement;
            // ReSharper disable once Unity.PerformanceCriticalCodeCameraMain
            camera ??= Camera.main;
            if (camera)
            {
                Vector3 flat = new Vector3(input.x, 0, input.y);
                Vector3 cameraForward = camera.transform.forward.FlattenDirection();
                Vector3 cameraRight = camera.transform.right.FlattenDirection();
                Vector3 adjusted = cameraForward * flat.z + cameraRight * flat.x; 
                // TODO - Adjusted curve
                Vector3 target = adjusted * properties.TargetSpeed;
                ChangeMovement(target);
            }
            else
            {
                Vector3 target = new Vector3(input.x, 0, input.y) * properties.TargetSpeed;
                ChangeMovement(target);
            }
        }

        protected virtual void UpdateRotation()
        {
            if(velocity.sqrMagnitude > 0)
            {
                ChangeRotation(velocity.normalized);
            }
        }

        protected virtual void ChangeRotation(Vector3 direction)
        {
            MovementProperties properties = characterController.isGrounded ? groundedMovement : inAirMovement;
            Vector3 look = Vector3.RotateTowards(transform.forward.FlattenDirection(), 
                                                 direction.normalized.FlattenDirection(), 
                                                 properties.TargetRotation * Time.deltaTime, 0);
            transform.forward = look;
        }

        private void UpdateGravity()
        {
            if (!hasGravity)
            {
                return;
            }
            
            if (characterController.isGrounded)
            {
                velocity.y = Mathf.Max(0, velocity.y);
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }
        }

        #endregion
        
        #region Jump

        private void OnJumpAction(InputAction.CallbackContext context)
        {
            Jump();
        }

        private void Jump()
        {
            // TODO - This is just about the most simple a jump can be.
            velocity.y = jumpVelocity;
        }
        
        #endregion
    }
}