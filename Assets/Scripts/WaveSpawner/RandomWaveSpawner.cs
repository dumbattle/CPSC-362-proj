using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveSpawner : WaveSpawner {
    public float countGrowthA = 100;
    public float countGrowthB = 100;
    public float waveGrowthA = 10;
    public float waveGrowthB = 10;

    public override int MaxWave => 999;


    [SerializeField]
    [ArrayLabel("Entry")]
    Entry[] entries;

    protected override WaveContents GetWaveContents(int w) {
        return new RWaveContents(
            from x in entries where x.initialWave <= w select x,
            (1 + countGrowthA * (Mathf.Log(w + countGrowthB) - Mathf.Log(countGrowthB))),
            (int)(1 + waveGrowthA * (Mathf.Log(w + waveGrowthB) - Mathf.Log(waveGrowthB))));
    }

    [Serializable]
    class Entry {
        public CreepBehaviour Creep = default;
        [Min(.1f)]
        public float seperation = default;
        public float count = default;
        [Min(0)]
        public int initialWave = default;
    }

    class RWaveContents : WaveContents {
        IEnumerable<Entry> entries;
        float countMul;
        int numWaves;

        Entry currentEntry;
        int currCount;
        float maxCount;
        int currentWave;

        public RWaveContents(IEnumerable<Entry> entries, float countMul, int numWaves) {
            this.entries = entries;
            this.countMul = countMul;
            this.numWaves = numWaves;
        }

        public override (CreepBehaviour creep, float delay) Next() {
            if (currentEntry == null || currCount >= maxCount) {
                currentEntry = entries.SampleRandom();
                maxCount = currentEntry.count * countMul;
                currentWave++;
                currCount = 0;
                if (currentWave > numWaves) {
                    return (null, 1);
                }
            }

            currCount++;
            return (currentEntry.Creep, currentEntry.seperation);
        }

        public override void Reset() {
            currentEntry = null;
            currCount = 0;
            currentWave = 0;
        }
    }

}