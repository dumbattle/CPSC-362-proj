using System;
using UnityEngine;

[Serializable]
public abstract class WaveContents {
    public abstract (CreepBehaviour creep, float delay) Next();
    public abstract void Reset();
    public abstract bool Done();
}

[Serializable]
public class PresetWaveContents : WaveContents {
    [SerializeField]
    Entry[] creeps = null;


    int current = 0;

    public override (CreepBehaviour creep, float delay) Next() {
        if (Done()) {
            return (null, 1);
        }
        var result = (creeps[current].creep, creeps[current].delay);

        current++;
        return result;
    }

    public override void Reset() {
        current = 0;
    }

    public override bool Done() {
        return current >= creeps.Length;
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