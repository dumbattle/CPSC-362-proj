using System;
using UnityEngine;

[Serializable]
public abstract class WaveContents {
    public abstract (CreepBehaviour creep, float delay) Next();
    public abstract void Reset();
}
