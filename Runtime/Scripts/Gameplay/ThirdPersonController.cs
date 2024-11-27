using Fsi.Gameplay.Extensions;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace Fsi.Gameplay.Gameplay
{
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Visuals")]

        [SerializeField]
        private CharacterVisuals visuals;
        
        [Header("Physics")]

        [SerializeField]
        private new Rigidbody rigidbody;

        protected Rigidbody Rigidbody => rigidbody;
        
        // Movement
        private Vector3 velocity;
        
        [Header("Movement")]

        [SerializeField]
        private float accel = 100;

        [SerializeField]
        private float deccel = 50f;

        [SerializeField]
        private float movementSpeed = 10;
        
        [SerializeField]
        private float rotationSpeed = 100f;
        
        [Header("Jump")]
        
        [Min(0)]
        [SerializeField]
        private float jumpForce = 100f;
        
        // Camera
        private new Camera camera;
        
        protected virtual void FixedUpdate()
        {
            UpdateMovement();
            UpdateRotation();
        }

        #region Movement
        
        protected virtual void UpdateMovement()
        {
            Vector3 move = velocity * Time.deltaTime;
            Vector3 movePosition = move + transform.position;
            rigidbody.MovePosition(movePosition);

            if (visuals)
            {
                visuals.SetMovement(velocity);
            }
        }

        protected virtual void ChangeMovement(Vector3 target)
        {
            velocity = velocity.sqrMagnitude >= target.sqrMagnitude 
                           ? Vector3.MoveTowards(velocity, target, accel * Time.deltaTime) 
                           : Vector3.MoveTowards(velocity, target, deccel * Time.deltaTime);
        }
        
        public virtual void ProvideMovementInput(Vector2 input)
        {
            camera ??= Camera.main;
            if (camera)
            {
                Vector3 flat = new Vector3(input.x, 0, input.y);
                Vector3 cameraForward = camera.transform.forward.FlattenDirection();
                Vector3 cameraRight = camera.transform.right.FlattenDirection();
                Vector3 adjusted = cameraForward * flat.z + cameraRight * flat.x; 
                adjusted.Normalize();
                Vector3 target = adjusted * movementSpeed;
                ChangeMovement(target);
            }
            else
            {
                Vector3 target = new Vector3(input.x, 0, input.y) * movementSpeed;
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
            Vector3 look = Vector3.RotateTowards(transform.forward.FlattenDirection(), 
                                                 direction.normalized.FlattenDirection(), 
                                                 rotationSpeed * Time.deltaTime, 0);
            rigidbody.MoveRotation(Quaternion.LookRotation(look));
        }

        #endregion
        
        #region Jump
        
        public void Jump()
        {
            Rigidbody.AddForce(Vector3.up * jumpForce);
        }
        
        #endregion
    }
}