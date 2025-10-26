using UnityEditor;
using UnityEngine;

namespace Fsi.Gameplay.Arrows
{
	public class ForwardArrowEditor : Editor
	{
		// [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		// protected static void DrawCreatureGizmos(Transform transform, GizmoType gizmoType)
		// {
		// 	if (!transform)
		// 	{
		// 		Debug.LogWarning("Transform is null.");
		// 		return;
		// 	}
		//
		// 	DrawArrow(transform.position +  transform.up * 1f, transform.forward, Color.white);
		// }
		
		// [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		// private static void DrawArrow(Transform transform, GizmoType gizmoType)
		// {
		// 	Handles.color = color;
		// 	Handles.DrawLine(position, position + forward, forward.magnitude);
		// }
	}
}