using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Gameplay.Buckets
{
	[CustomPropertyDrawer(typeof(Bucket<,>), true)]
	public class BucketDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement root = new();
			
			root.Add(new PropertyField(property));
			root.Add(new Spacer());
			
			return root;
		}
	}
}