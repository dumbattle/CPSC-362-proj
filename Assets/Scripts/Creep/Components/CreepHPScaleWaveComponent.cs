using UnityEngine;

public class CreepHPScaleWaveComponent:CreepCommponent {
    [Min(0)]
    public float scalePerWave;

    public override void Init(CreepBehaviour cb) {
        float p = 1;
        for (int i = 0; i < WaveSpawner.currentWave; i++) {
            p *= 1 + scalePerWave;
        }
        cb.health.SetMaxHP((int)(cb.health.max * p), true);
    }
}
