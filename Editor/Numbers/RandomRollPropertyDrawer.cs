using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Gameplay.Numbers
{
	[CustomPropertyDrawer(typeof(RandomRoll))]
	public class RandomRollPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement root = new()
			                     {
				                     style =
				                     {
					                     paddingLeft = property.depth * 15
				                     }
			                     };

			SerializedProperty valueProp = property.FindPropertyRelative(nameof(RandomRoll.roll));

			var valueField = new Slider
			                 {
				                 label = property.displayName,
				                 lowValue = 0f,
				                 highValue = 1f,
				                 showInputField = true
			                 };

			valueField.BindProperty(valueProp);

			root.Add(valueField);
			return root;
		}
	}
}