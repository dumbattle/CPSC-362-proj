/* Reference: 
 *   https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/ 
 */


using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ArrayLabelAttribute))]
public class NamedArrayDrawer : PropertyDrawer {
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {

        if (int.TryParse(property.propertyPath.Split('[', ']')[1], out int pos)) {
            string text = ((ArrayLabelAttribute)attribute).label + $" {pos.ToString()}";
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