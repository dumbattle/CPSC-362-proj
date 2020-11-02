using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TimeBasedWaveSpawner : WaveSpawner {
    [ArrayLabel("Wave")]
    public Content[] waves;


    public override int MaxWave => waves.Length - 1;

    protected override WaveContents GetWaveContents(int w) {
        if (w >= waves.Length) {
            return null;
        }

        return waves[w];
    }



    [System.Serializable]
    public class Content : WaveContents {
        [SerializeField]
        [ArrayLabel("Entry")]
        Entry[] entries = null;

        IEnumerator<(CreepBehaviour c, float d)> currentContents;

        public override void Reset() {
            currentContents = GetContents();
        }

        IEnumerator<(CreepBehaviour c, float d)> GetContents() {
            CreepBehaviour[] creeps = new CreepBehaviour[entries.Length];
            LinkedList<float>[] times = new LinkedList<float>[entries.Length];

            for (int i = 0; i < entries.Length; i++) {
                creeps[i] = entries[i].creep;
                times[i] = (from x in entries[i].time orderby x ascending select x).ToLinkedList();
            }
            float totalTime = 0;
            while (true) {
                float minDelay = -1;
                CreepBehaviour creep = null;
                int resultI = -1;

                for (int i = 0; i < entries.Length; i++) {
                    if (times[i].Count == 0) {
                        continue;
                    }

                    if (times[i].First.Value - totalTime < minDelay || minDelay < 0) {
                        creep = creeps[i];
                        minDelay = times[i].First.Value - totalTime;
                        resultI = i;
                    }
                }

                if (resultI >= 0) {
                    times[resultI].RemoveFirst();
                    totalTime += minDelay;
                    yield return (creep, minDelay);
                }
                else {
                    yield return (null, 1);
                }
            }
        }

        public override (CreepBehaviour creep, float delay) Next() {
            if (currentContents.MoveNext()) {
                return currentContents.Current;
            }

            return (null, 1);
        }

        [System.Serializable]
        struct Entry {
            public CreepBehaviour creep;
            [ArrayLabel("T")]
            public float[] time;

            public Entry(CreepBehaviour creep, float[] time) {
                this.creep = creep;
                this.time = time;
            }
        }
    }
}
