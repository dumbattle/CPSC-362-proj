using UnityEngine;

public class CreepTester : MonoBehaviour {
    public CreepBehaviour creep;

    [Min(1)]
    public int count = 1;

    CreepBehaviour[] creeps;


    void Awake() {
        creep.gameObject.SetActive(false); // deactivate source

        creeps = new CreepBehaviour[count];

        for (int i = 0; i < count; i++) {
            creeps[i] = Instantiate(creep);
            creeps[i].Init(); 
            creeps[i].gameObject.SetActive(true); // copied from a deactivated creep, so must be activated
        }
    }

    void Update() {
        foreach (var c in creeps) {
            c.GameplayUpdate(); // creeps don't update themselves. If you disable this script, the creeps will(should) do nothing
        }
    }
}