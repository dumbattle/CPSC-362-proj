using System;
using System.Collections;
using UnityEngine;

public abstract class WaveSpawner : MonoBehaviour {
    CreepManager creepManager;

    IEnumerator spawnLoop;
    bool _done;

    public bool spawningDone => _done;
   
    public void SetWave(int waveNum) {
        _done = false;
        spawnLoop = SpawnWave(GetWaveContents(waveNum));
    }

    public void Init(CreepManager cm) {
        creepManager = cm;
    }

    public void SpawnUpdate() {
        _done = !spawnLoop.MoveNext();
    }

    IEnumerator SpawnWave(WaveContents wc) {
        if (wc == null) {
            yield break;
        }
        wc.Reset();


        CreepBehaviour cb;
        float delay;
        (cb, delay) = wc.Next();

        float timer = 0;
        while (cb != null) {
            timer -= .01f;

            int safety = 0;
            while (timer < 0 && cb != null) {
                safety++;
                if (safety > 1000) {
                    // infinite loops in Unity3D are traumatizing
                    // I'm not kidding
                    throw new OverflowException("Safety reached");
                }

                creepManager.SpawnCreep(cb);
                //var newCreep = Instantiate(em, transform.position, Quaternion.identity);
                //newCreep.gameObject.SetActive(true);
                (cb, delay) = wc.Next();
                timer += delay;
            }

            yield return null;
        }
        _done = true;
    }

    protected abstract WaveContents GetWaveContents(int w);
}
