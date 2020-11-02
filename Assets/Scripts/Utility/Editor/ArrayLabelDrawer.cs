/* Reference: 
 *   https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/ 
 *   
 *   I used the answers on this site as a base for this script
 */


using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ArrayLabelAttribute))]
public class ArrayLabelDrawer : PropertyDrawer {
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
        var s = property.propertyPath.Split('[', ']');

        if (int.TryParse(s[s.Length - 2], out int pos)) {
            var attr = ((ArrayLabelAttribute)attribute);

            string text = attr.label + $" {(pos + (attr.zeroBasedIndex ? 0 : 1)).ToString()}";
            EditorGUI.PropertyField(rect, property, new GUIContent(text), true);
        }
        else {
            EditorGUI.PropertyField(rect, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUI.GetPropertyHeight(property);
    }
}