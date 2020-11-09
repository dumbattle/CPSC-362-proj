using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepDeathSoundComponent : CreepCommponent
{
    public AudioClip clip;

    public override void Init(CreepBehaviour cb)
    {
        base.Init(cb);
        cb.OnKilled += PlayAudio; 
    }

    private void PlayAudio()
    {
        SoundManager.Play(clip);
    }
}
