using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveSpawner : WaveSpawner {
    public float growth;
    public float groupStrength;

    public override int MaxWave => 999;


    [SerializeField]
    [ArrayLabel("Entry")]
    Entry[] entries;

    private void Awake() {
        
    }

    protected override WaveContents GetWaveContents(int w) {
        return new RWaveContents(
            from x in entries where x.initialWave <= w select x,
            50 * (1 + 5 * (Mathf.Log(w + 100) - Mathf.Log(100))),
            (int)(1 + 10 * (Mathf.Log(w + 10) - Mathf.Log(10))));
    }

    [Serializable]
    class Entry {
        public CreepBehaviour Creep;
        [Min(.1f)]
        public float seperation;
        [Min(1)]
        public float strength;
        [Min(0)]
        public int initialWave;
    }

    class RWaveContents : WaveContents {
        IEnumerable<Entry> entries;
        float str;
        int numWaves;

        Entry currentEntry;
        float currentStr;
        int currentWave;

        public RWaveContents(IEnumerable<Entry> entries, float totalStrength, int numWaves) {
            this.entries = entries;
            str = totalStrength;
            this.numWaves = numWaves;
        }

        public override (CreepBehaviour creep, float delay) Next() {
            if (currentEntry == null || currentStr > str) {
                currentEntry = entries.SampleRandom();
                currentStr = 0;
                currentWave++;

                if (currentWave > numWaves) {
                    return (null, 1);
                }
            }

            currentStr += currentEntry.strength;
            return (currentEntry.Creep, currentEntry.seperation);
        }

        public override void Reset() {
            currentEntry = null;
            currentStr = 0;
            currentWave = 0;
        }
    }

}