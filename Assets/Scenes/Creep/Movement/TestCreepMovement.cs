using UnityEngine;

public class TestCreepMovement : CreepMovement {
    public float speed = 5;
    Vector3 dest;

    public override void GameplayUpdate() {
        var dir = dest - transform.position;

        if (dir.sqrMagnitude < .3f) { // close
            dest = Random.insideUnitCircle * 5; // new dest
            GameplayUpdate(); // retry movement
            return;
        }

        transform.position += dir.normalized * speed * Time.deltaTime; 
    }
}