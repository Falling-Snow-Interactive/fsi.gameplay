using UnityEditor;
using UnityEngine;

namespace Fsi.Gameplay.Arrows
{
	public class ForwardArrow : MonoBehaviour
	{
		[SerializeField]
		private float size = 1f;

		[SerializeField]
		private float height = 1f;
		
		[SerializeField]
		private Color color = Color.white;

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Handles.color = color;
			Handles.DrawLine(transform.position + transform.up * height, 
			                 transform.position + transform.forward * size + transform.up * height, 
			                 size);
			Handles.ConeHandleCap(-1, 
			                      transform.position + transform.forward * size + transform.up * height,
			                      transform.rotation, 
			                      size / 5f,
			                      EventType.Repaint);
		}
		#endif
	}
}