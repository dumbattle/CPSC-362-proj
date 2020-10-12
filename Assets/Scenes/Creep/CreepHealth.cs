using UnityEngine;


public class CreepHealth : MonoBehaviour {
    public int max { get; private set; }
    public int current { get; private set; }

    public void Init(int maxHP) {
        max = current = maxHP;
    }

    // TODO - add functionality
}
