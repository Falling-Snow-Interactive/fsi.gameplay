using System;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace Fsi.Gameplay.Physics
{
	[AddComponentMenu("Falling Snow Interactive/Gameplay/Ground Check")]
	public class GroundCheck : MonoBehaviour
	{
		public event Action Landed;
		public event Action Jumped;
		
		[SerializeField]
		private float radius = 0.35f;

		[SerializeField]
		private Vector3 offset;

		[SerializeField]
		private LayerMask mask;

		[SerializeField]
		private CharacterVisuals character;

		private bool IsGrounded { get; set; }

		private void Start()
		{
			Vector3 position = transform.position + transform.forward * offset.z
			                                      + transform.right * offset.x
			                                      + transform.up * offset.y;
			IsGrounded = UnityEngine.Physics.CheckSphere(position, radius, mask);
			character.SetGrounded(true);
		}

		private void FixedUpdate()
		{
			Vector3 position = transform.position + transform.forward * offset.z
			                                      + transform.right * offset.x
			                                      + transform.up * offset.y;
			bool isGrounded = UnityEngine.Physics.CheckSphere(position, radius, mask);
			switch (IsGrounded)
			{
				case true when !isGrounded:
					Jumped?.Invoke();
					break;
				case false when isGrounded:
					Landed?.Invoke();
					break;
			}
			IsGrounded = isGrounded;
			character.SetGrounded(IsGrounded);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = IsGrounded ? Color.green : Color.red;
			Vector3 position = transform.position + transform.forward * offset.z
			                                      + transform.right * offset.x
			                                      + transform.up * offset.y;
			Gizmos.DrawWireSphere(position, radius);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = IsGrounded ? Color.green : Color.red;
			Vector3 position = transform.position + transform.forward * offset.z
			                                      + transform.right * offset.x
			                                      + transform.up * offset.y;
			Gizmos.DrawSphere(position, radius);
		}
	}
}