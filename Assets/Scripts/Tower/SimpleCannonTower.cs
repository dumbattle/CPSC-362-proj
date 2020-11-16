using System.Linq;
using System.Collections;
using UnityEngine;


public class SimpleCannonTower : ProjectileTower {
    [Min(0)]
    public int damage;
    [Min(0)]
    public float radius = 1;
    [Range(0f,1f)]
    public float damageScaleAtMaxRadius = 1f;

    [Header("Splash Settings")]
    public SpriteRenderer explosionObj;
    public bool fade = true;
    [Min(0)]
    public float visualLifetime = 1;


    public override void Effect(CreepBehaviour cb, Vector2 lastPos) {
        Vector3 center;
        if (cb != null) {
            center = cb.transform.position;
        }
        else {
            center = lastPos; 
        }

        foreach (var c in CreepManager.main.AllCreeps()) {
            var d = (c.transform.position - center).magnitude;

            if (d < radius) {
                var scale = 1 - d / radius;
                scale = (1 - damageScaleAtMaxRadius) * scale + damageScaleAtMaxRadius;

                int dmg = (int)(damage * scale);

                c.health.damage(dmg);
            }
        }

        GlobalGameplayUpdate.AddGameplayWaitUpdate(XplosionAnim(center)); // remove slow after some time
    }

    IEnumerator XplosionAnim(Vector3 pos) {
        float timer = 0;
        var obj = Instantiate(explosionObj);
        obj.gameObject.SetActive(true);

        obj.transform.position = pos;
        obj.transform.localScale = new Vector3(0, 0, 1);
        //FaceTarget(cb);
        while (timer < visualLifetime) {
            timer += Time.deltaTime;

            var s = timer / visualLifetime;


            obj.transform.localScale = new Vector3(s * radius, s * radius, 1);
            if (fade) {
                obj.color = obj.color.SetAlpha(1 - s);
            }
            yield return null;
        }

        obj.gameObject.SetActive(false);
        Destroy(obj);
    }
}
