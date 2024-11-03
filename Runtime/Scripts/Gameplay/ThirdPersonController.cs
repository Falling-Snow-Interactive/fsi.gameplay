using Fsi.Gameplay.Visuals;
using Fsi.Prototyping.Extensions;
using UnityEngine;

namespace Fsi.Prototyping.Gameplay
{
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Visuals")]

        [SerializeField]
        private CharacterVisuals visuals;
        
        [Header("Physics")]

        [SerializeField]
        private new Rigidbody rigidbody;
        
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
        
        // Camera
        private new Camera camera;
        
        private void FixedUpdate()
        {
            UpdateMovement();
            UpdateRotation();
        }

        #region Movement
        
        private void UpdateMovement()
        {
            Vector3 move = velocity * Time.deltaTime;
            Vector3 movePosition = move + transform.position;
            rigidbody.MovePosition(movePosition);

            if (visuals)
            {
                visuals.SetMovement(velocity);
            }
        }

        private void ChangeMovement(Vector3 target)
        {
            velocity = velocity.sqrMagnitude >= target.sqrMagnitude 
                           ? Vector3.MoveTowards(velocity, target, accel * Time.deltaTime) 
                           : Vector3.MoveTowards(velocity, target, deccel * Time.deltaTime);
        }
        
        public void ProvideMovementInput(Vector2 input)
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

        private void UpdateRotation()
        {
            if(velocity.sqrMagnitude > 0)
            {
                ChangeRotation(velocity.normalized);
            }
        }

        private void ChangeRotation(Vector3 direction)
        {
            Vector3 look = Vector3.RotateTowards(transform.forward.FlattenDirection(), 
                                                 direction.normalized.FlattenDirection(), 
                                                 rotationSpeed * Time.deltaTime, 0);
            rigidbody.MoveRotation(Quaternion.LookRotation(look));
        }

        #endregion
    }
}