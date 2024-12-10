using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

namespace Fsi.Gameplay.Visuals
{
    public class CharacterVisuals : MonoBehaviour
    {
        [Header("Animation")]
        
        [SerializeField]
        private Animator animator;

        protected Animator Animator => animator;
        
        [Serializable]
        private struct BoneReference
        {
            public string tag;
            public Transform bone;
        }
        
        // Bones
        [Header("Bones")]
        
        [SerializeField]
        private List<BoneReference> boneReferences = new List<BoneReference>();
        public Dictionary<string, Transform> Bones { get; private set; }

        [Header("Parameters")]

        [SerializeField]
        private string moveXParam = "MoveX";
        
        [SerializeField]
        private string moveZParam = "MoveZ";
        
        [SerializeField]
        private string groundedParam = "Grounded";

        [SerializeField]
        private string deadParam = "Dead";
        
        [SerializeField]
        private string victoryParam = "Victory";

        private void Awake()
        {
            Bones = new Dictionary<string, Transform>();
            foreach (var boneReference in boneReferences)
            {
                Bones.Add(boneReference.tag, boneReference.bone);
            }
        }
        
        public void SetMovement(Vector3 velocity)
        {
            Vector3 inverse = transform.InverseTransformDirection(velocity.normalized) * velocity.magnitude;
            
            animator.SetFloat(moveXParam, inverse.x);
            animator.SetFloat(moveZParam, inverse.z);
        }

        public void SetGrounded(bool set)
        {
            animator.SetBool(groundedParam, set);
        }

        public void SetDead(bool set)
        {
            animator.SetBool(deadParam, set);
        }

        public void SetVictory(bool set)
        {
            animator.SetBool(victoryParam, set);
        }
    }
}
