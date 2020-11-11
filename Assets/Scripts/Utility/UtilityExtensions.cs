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

    public static Color SetAlpha(this Color c, float a) {
        c.a = a;
        return c;
    }
    public static Vector3Int SetZ(this Vector2Int v, int z) {
        return new Vector3Int(v.x, v.y, z);
    }
}
