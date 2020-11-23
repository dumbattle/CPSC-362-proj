using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;



public class SampleTower : TowerBehaviour {
    public Targeting targeting;
    [Min(.001f)]
    public float coolDown;
    private float cooldownTimer;
    public float range = 2f;              

    public LineRenderer line;
    [Range(.1f, 3f)]
    public float LaserLife = 1;

    [Min(1)] public int damageDone = 1;
    public Sound contactSound;


    public override void GameplayUpdate() {
        FadeLaser();
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0) {
            return;
        }
        var target = targeting.GetTarget(transform.position, range);            

        // If the closest enemy is in range, that enemy becomes the target
        if (target != null ) {
            FaceTarget(target);
            cooldownTimer = coolDown;
            FireLaser(target);
        }

    }

    private void FireLaser(CreepBehaviour target) {
        // the line renderer is enabled when a target is detected within range
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
        var creepyBar = target.GetComponent<CreepHealth>();
        creepyBar.damage(damageDone);
        StartLaser();
        LazerTowerSoundManager.Play(contactSound);
    }

    public override void WaitUpdate() {
        FadeLaser();
    }

    private void FadeLaser() {
        var c = line.startColor;
        var f = 1 / LaserLife;
        c.a = c.a - f * Time.deltaTime;
        line.startColor = c;
        line.endColor = c;
    }


    private void StartLaser() {
        var c = line.startColor;
        c.a = 1;
        line.startColor = c;
        line.endColor = c;
    }
}
