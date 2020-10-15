using System;
using System.Collections;
using UnityEngine;

public abstract class WaveSpawner : MonoBehaviour {

    IEnumerator spawnLoop;
    bool _done;

    public bool spawningDone => _done;
   
    public void SetWave(int waveNum) {
        _done = false;
        spawnLoop = SpawnWave(GetWaveContents(waveNum));
    }



    public void GameplayUpdate() {
        _done = !spawnLoop.MoveNext();
    }

    IEnumerator SpawnWave(WaveContents wc) {
        if (wc == null) {
            yield break;
        }
        wc.Reset();


        EnemyMovement em;
        float delay;
        (em, delay) = wc.Next();

        float timer = 0;
        while (em != null) {
            timer -= .01f;

            int safety = 0;
            while (timer < 0 && em != null) {
                safety++;
                if (safety > 1000) {
                    // infinite loops in Unity3D are traumatizing
                    // I'm not kidding
                    throw new OverflowException("Safety reached");
                }


                var newCreep = Instantiate(em, transform.position, Quaternion.identity);
                newCreep.gameObject.SetActive(true);
                (em, delay) = wc.Next();
                timer += delay;
            }

            yield return null;
        }
        _done = true;
    }

    protected abstract WaveContents GetWaveContents(int w);
}
