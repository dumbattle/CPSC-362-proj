using UnityEngine;

public class CreepRotatorComponent : CreepCommponent {
    public float rotationSpeed;

    public override void GameplayUpdate() {
        // spin
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime), Space.Self);
    }
}