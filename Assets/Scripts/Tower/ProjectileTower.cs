using System.Collections;
using UnityEngine;

public abstract class ProjectileTower : TowerBehaviour {
    public Targeting targeting;
    public GameObject projectile;
    [Min(.01f)]
    public float projectileSpeed = 1;

    [Min(.001f)]
    public float coolDown;
    public float range = 2f;

    private float cooldownTimer = 0;

    public override void GameplayUpdate() {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0) {
            return;
        }

        CreepBehaviour target = targeting.GetTarget(transform.position, range);

        if (target != null) {
            cooldownTimer = coolDown;
                FaceTarget(target);
            FireProjectile(target);
        }
    }

    private void FireProjectile(CreepBehaviour target) {
        var p = Instantiate(projectile);
        p.gameObject.SetActive(true);
        p.transform.position = transform.position;

        var pu = ProjectileUpdate(p, target);
        GlobalGameplayUpdate.AddGameplayWaitUpdate(pu);
    }

    IEnumerator ProjectileUpdate(GameObject proj, CreepBehaviour target) {
        // error prevention
        if (target == null) {
            yield break;
        } 

        // last known target position
        Vector3 pos = target.transform.position;

        while (true) {
            var dir = GetTarget() - proj.transform.position;

            // reached target
            if (dir.sqrMagnitude < (projectileSpeed * Time.deltaTime) * (projectileSpeed * Time.deltaTime)) {
                Effect(target, pos);

                Destroy(proj);
                yield break;
            }

            proj.transform.position += dir.normalized * Time.deltaTime * projectileSpeed;
            proj.transform.up = dir;
            yield return null;
        }

        Vector3 GetTarget() {
            // if target is destroyed, we use the last position instead
            if (target != null) { 
                pos = target.transform.position;
            }
            return pos;
        }
    }

    // what happens when the projectile makes contact
    public abstract void Effect(CreepBehaviour cb, Vector2 lastPos);


    public override void WaitUpdate() { }
}
