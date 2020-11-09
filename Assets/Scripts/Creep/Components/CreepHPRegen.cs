using UnityEngine;

public class CreepHPRegen : CreepCommponent {
    public float scalePerSec;

    float amnt = 0;
    CreepHealth health;

    public override void Init(CreepBehaviour cb) {
        health = cb.health;
    }

    public override void GameplayUpdate() {
        amnt += health.max * scalePerSec * Time.deltaTime;

        if (amnt >= 1) {
            health.Heal((int)amnt);
            amnt %= 1f;
        }
    }
}