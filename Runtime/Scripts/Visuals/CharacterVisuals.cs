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

        [SerializeField]
        private string punchParam = "Punch";
        
        [SerializeField]
        private string kickParam = "Kick";

        [SerializeField]
        private string spellParam = "Spell";

        [SerializeField]
        private string hitParam = "Hit";

        [SerializeField]
        private string deadParam = "Dead";
        
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

        public void Punch()
        {
            animator.SetTrigger(punchParam);
        }

        public void Kick()
        {
            animator.SetTrigger(kickParam);
        }

        public void Spell()
        {
            animator.SetTrigger(spellParam);
        }

        public void Hit()
        {
            animator.SetTrigger(hitParam);
        }

        public void Dead(bool set)
        {
            animator.SetBool(deadParam, set);
        }
    }
}
