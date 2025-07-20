using Fsi.Gameplay.Numbers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Numbers
{
	[CustomPropertyDrawer(typeof(Percent))]
	public class PercentPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement root = new();

			SerializedProperty valueProp = property.FindPropertyRelative("value");

			Slider valueField = new Slider
			                    {
				                        label = property.displayName,
				                        lowValue = 0f,
				                        highValue = 1f,
				                        showInputField = true,
			                    };

			valueField.BindProperty(valueProp);

			root.Add(valueField);
			return root;
		}
	}
}