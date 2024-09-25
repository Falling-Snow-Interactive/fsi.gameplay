using UnityEngine;

namespace Fsi.Prototyping.Visuals
{
    public class CharacterVisuals : MonoBehaviour
    {
        [Header("Animation")]
        
        [SerializeField]
        private Animator animator;

        [Header("Parameters")]

        [SerializeField]
        private string movingParam = "Moving";

        [SerializeField]
        private string moveXParam = "MoveX";

        [SerializeField]
        private string moveZParam = "MoveZ";

        [SerializeField]
        private string groundedParam = "IsGrounded";
        
        public void SetMovement(Vector3 velocity)
        {
            Vector3 inverse = transform.InverseTransformDirection(velocity.normalized) * velocity.magnitude;
            
            animator.SetFloat(moveXParam, inverse.x);
            animator.SetFloat(moveZParam, inverse.z);
            
            animator.SetBool(movingParam, velocity.sqrMagnitude > 0);
        }

        public void SetGrounded(bool set)
        {
            animator.SetBool(groundedParam, set);
        }
    }
}
