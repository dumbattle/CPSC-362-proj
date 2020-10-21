using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AustinsExamples.UI {
    public abstract class UIManager : MonoBehaviour {
        // input data
        public static Color color { get; protected set; } = new Color(1, 1, 1, 1);
        public static Vector2Int tile { get; protected set; }

        // we need to know if the input is in use
        public static bool colorRecieved { get; protected set; }
        public static bool tileRecieved { get; protected set; }


        // this should be called every frame
        // i.e. input only lasts 1 frame
        protected virtual void Clear() {
            colorRecieved = false;
            tileRecieved = false;
            // color is not cleared since colorRecieved already signals if this should be read
            // tile is not cleared for the same reason
        }


        // derived classes of UIManager will use this Register class to signal that input data has been obtained
        // this does not need to be in a class, but I do this for organization
        protected static class Register {
            public static void Color(Color c) {
                colorRecieved = true;
                color = c;
            }

            // 2 ways of passing a tile
            public static void Tile(int x, int y) {
                tileRecieved = true;
                tile = new Vector2Int(x, y);
            }
            public static void Tile(Vector2Int t) {
                tileRecieved = true;
                tile = t;
            }
        }
    }
}