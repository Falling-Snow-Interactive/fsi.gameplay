using UnityEngine;

namespace Fsi.Gameplay
{
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionField { get; }
        public object CompareValue { get; }

        public ShowIfAttribute(string conditionField, object compareValue)
        {
            ConditionField = conditionField;
            CompareValue = compareValue;
        }
    }
}