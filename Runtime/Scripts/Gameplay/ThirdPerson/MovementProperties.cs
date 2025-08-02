using System;
using UnityEngine;

namespace Fsi.Gameplay.Gameplay.ThirdPerson
{
	[Serializable]
	public class MovementProperties
	{
		[SerializeField]
		private float accel = 100;

		[SerializeField]
		private float deccel = 50f;

		[SerializeField]
		private float targetSpeed = 10;

		[SerializeField]
		private float targetRotation = 100f;
		public float Accel => accel;
		public float Deccel => deccel;
		public float TargetSpeed => targetSpeed;
		public float TargetRotation => targetRotation;
	}
}