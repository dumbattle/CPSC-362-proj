using UnityEngine;

public class ArrayLabelAttribute : PropertyAttribute {
    public string label;
    public bool zeroBasedIndex;

    public ArrayLabelAttribute(string label) : this(label, true){ }
    public ArrayLabelAttribute(string label, bool zeroBasedIndex) {
        this.label = label;
        this.zeroBasedIndex = zeroBasedIndex;
    }
}
