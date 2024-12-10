using System;
using UnityEngine;

namespace Fsi.Gameplay.Visuals
{
    public class SpriteCharacterVisuals : CharacterVisuals
    {
        [Header("Sprite Control")]
        
        [SerializeField]
        private bool lockDirection = true;
        
        [SerializeField]
        private Vector3 direction = Vector3.forward;

        private void LateUpdate()
        {
            transform.forward = direction;
        }
    }
}
