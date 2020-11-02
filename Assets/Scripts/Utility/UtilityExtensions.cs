using System.Collections.Generic;
using UnityEngine;

// General purpose extensions
public static class UtilityExtensions {
    public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> src) {
        LinkedList<T> result = new LinkedList<T>();

        foreach (var item in src) {
            result.AddLast(item);
        }

        return result;
    }
}

public class ArrayLabelAttribute : PropertyAttribute {
    public string label;
    public ArrayLabelAttribute(string label) {
        this.label = label;
    }
}
