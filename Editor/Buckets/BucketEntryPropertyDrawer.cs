using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Gameplay.Buckets
{
    public class BucketEntryPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            
            root.style.flexDirection = FlexDirection.Row;
            root.style.flexGrow = 1;
            // root.style.flexShrink = 1;
            
            SerializedProperty valueProp = property.FindPropertyRelative("value");
            PropertyField valueField = new PropertyField(valueProp)
                                       {
                                           label = "",
                                           style =
                                           {
                                               flexGrow = 1,
                                           }
                                       };

            SerializedProperty weightProp = property.FindPropertyRelative("weight");
            PropertyField weightField = new PropertyField(weightProp)
                                        {
                                            label = "",
                                            style =
                                            {
                                                flexGrow = 0,
                                                width = 50,
                                            }
                                        };
            
            root.Add(valueField);
            root.Add(weightField);

            return root;
        }
    }
}