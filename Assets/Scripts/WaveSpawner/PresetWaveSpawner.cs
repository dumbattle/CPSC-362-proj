using System;
using UnityEngine;

public class PresetWaveSpawner : WaveSpawner {
    [SerializeField]
    PresetWaveContents[] waves = null;


    public override int MaxWave => waves.Length - 1;

    protected override WaveContents GetWaveContents(int w) {
        if (w >= waves.Length) {
            return null;
        }

        return waves[w];
    }

    [Serializable]
    class PresetWaveContents : WaveContents {
        [SerializeField]
        Entry[] creeps = null;


        int current = 0;

        public override (CreepBehaviour creep, float delay) Next() {
            if (current >= creeps.Length) {
                return (null, 1);
            }
            var result = (creeps[current].creep, creeps[current].delay);

            current++;
            return result;
        }

        public override void Reset() {
            current = 0;
        }

        [Serializable]
        struct Entry {
            public CreepBehaviour creep;
            public float delay;

            public Entry(CreepBehaviour creep, float delay) {
                this.creep = creep;
                this.delay = delay;
            }
        }
    }
}
