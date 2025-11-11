using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace Fsi.Gameplay
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var showIf = (ShowIfAttribute)attribute;
            var conditionProperty = FindConditionProperty(property, showIf.ConditionField);

            var field = new PropertyField(property);
            root.Add(field);

            void UpdateVisibility()
            {
                if (conditionProperty == null)
                {
                    // If the condition field can't be found, just show the property.
                    root.style.display = DisplayStyle.Flex;
                    return;
                }

                bool shouldShow = EvaluateCondition(conditionProperty, showIf.CompareValue);
                root.style.display = shouldShow ? DisplayStyle.Flex : DisplayStyle.None;
            }

            if (conditionProperty != null)
            {
                root.TrackPropertyValue(conditionProperty, _ => UpdateVisibility());
            }

            UpdateVisibility();
            return root;
        }

        private SerializedProperty FindConditionProperty(SerializedProperty property, string conditionField)
        {
            // Example property.propertyPath:
            // "steps.Array.data[0].npc"
            var path = property.propertyPath;
            var lastDot = path.LastIndexOf('.');
            if (lastDot < 0)
            {
                // Fallback: try root
                return property.serializedObject.FindProperty(conditionField);
            }

            var containerPath = path.Substring(0, lastDot + 1); // "steps.Array.data[0]."
            var fullPath = containerPath + conditionField;      // "steps.Array.data[0].stepType"
            return property.serializedObject.FindProperty(fullPath);
        }

        private bool EvaluateCondition(SerializedProperty conditionProperty, object compareValue)
        {
            switch (conditionProperty.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    if (compareValue == null)
                        return conditionProperty.boolValue;

                    if (compareValue is bool b)
                        return conditionProperty.boolValue == b;

                    break;

                case SerializedPropertyType.Enum:
                {
                    int targetIndex = -1;

                    if (compareValue == null)
                    {
                        return false;
                    }
                    else if (compareValue is int i)
                    {
                        targetIndex = i;
                    }
                    else if (compareValue is string enumName)
                    {
                        var names = conditionProperty.enumNames;
                        targetIndex = System.Array.IndexOf(names, enumName);
                    }
                    else if (compareValue.GetType().IsEnum)
                    {
                        // [ShowIf(nameof(stepType), StepType.NPC)]
                        string targetName = compareValue.ToString();
                        var names = conditionProperty.enumNames;
                        targetIndex = System.Array.IndexOf(names, targetName);
                    }

                    if (targetIndex >= 0)
                        return conditionProperty.enumValueIndex == targetIndex;

                    break;
                }

                case SerializedPropertyType.Integer:
                    if (compareValue is int i2)
                        return conditionProperty.intValue == i2;
                    break;

                case SerializedPropertyType.Float:
                    if (compareValue is float f)
                        return System.Math.Abs(conditionProperty.floatValue - f) < 0.0001f;
                    break;

                case SerializedPropertyType.String:
                    if (compareValue is string s2)
                        return conditionProperty.stringValue == s2;
                    break;
            }

            return false;
        }
    }
}