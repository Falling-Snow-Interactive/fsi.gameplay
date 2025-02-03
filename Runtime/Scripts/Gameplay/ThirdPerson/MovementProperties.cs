using System;
using UnityEngine;

namespace Fsi.Gameplay.Gameplay.ThirdPerson
{
    [Serializable]
    public class MovementProperties
    {
        [SerializeField]
        private float accel = 100;
        public float Accel => accel;

        [SerializeField]
        private float deccel = 50f;
        public float Deccel => deccel;

        [SerializeField]
        private float targetSpeed = 10;
        public float TargetSpeed => targetSpeed;
        
        [SerializeField]
        private float targetRotation = 100f;
        public float TargetRotation => targetRotation;
    }
}