﻿using System;
using System.Collections;
using UnityEngine;

public abstract class WaveSpawner : MonoBehaviour {
    public static int currentWave { get; private set; }
    CreepManager creepManager;

    IEnumerator spawnLoop;
    bool _done;

    public bool spawningDone => _done;
   
    public void SetWave(int waveNum) {
        currentWave = waveNum;
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
        if (wc == null || _done) {
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
                (cb, delay) = wc.Next();
                timer += delay;
            }

            yield return null;
        }
        _done = true;
    }

    protected abstract WaveContents GetWaveContents(int w);
    public abstract int MaxWave { get; }
}
