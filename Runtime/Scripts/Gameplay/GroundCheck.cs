using System;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace Fsi.Gameplay.Gameplay
{
    public class GroundCheck : MonoBehaviour
    {
        public event Action Landed;
        public event Action Jumped;
        
        public bool IsGrounded { get; private set; }

        [SerializeField]
        private float radius = 0.35f;

        [SerializeField]
        private Vector3 offset;

        [SerializeField]
        private LayerMask mask;

        [SerializeField]
        private CharacterVisuals character;

        private void Start()
        {
            Vector3 position = transform.position + transform.forward * offset.z 
                                                  + transform.right * offset.x 
                                                  + transform.up * offset.y;
            IsGrounded = Physics.CheckSphere(position, radius, mask);
            character.SetGrounded(true);
        }

        private void FixedUpdate()
        {
            Vector3 position = transform.position + transform.forward * offset.z 
                                                  + transform.right * offset.x 
                                                  + transform.up * offset.y;
            bool isGrounded = Physics.CheckSphere(position, radius, mask);
            if (IsGrounded && !isGrounded)
            {
                Jumped?.Invoke();
            }
            else if (!IsGrounded && isGrounded)
            {
                Landed?.Invoke();
            }
            IsGrounded = isGrounded;
            character.SetGrounded(IsGrounded);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Vector3 position = transform.position + transform.forward * offset.z 
                                                  + transform.right * offset.x 
                                                  + transform.up * offset.y;
            Gizmos.DrawSphere(position, radius);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Vector3 position = transform.position + transform.forward * offset.z 
                                                  + transform.right * offset.x 
                                                  + transform.up * offset.y;
            Gizmos.DrawWireSphere(position, radius);
        }
    }
}