using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class SlowProjectileTower : ProjectileTower {
    [Min(0)]
    public int damage;
    [Range(.1f, 1f)]
    public float slow = .5f;
    public float slowTime = 1;
    [Min(1)]
    public int maxStack = 1;

    public override void Effect(CreepBehaviour cb) {
        if (cb == null) {
            return;
        }
        cb.health.damage(damage);


        var count = EffectStacks.GetCreateEffect(cb, EffectString(), 0);
        EffectStacks.IncEffectCount(cb, EffectString());

        if (count < maxStack) {
            cb.movement.ModifySpeed(slow); // apply slow
        }

        GlobalGameplayUpdate.AddGameplayWaitUpdate(SlowTimer(cb, slow)); // remove slow after some time
    }

    IEnumerator SlowTimer(CreepBehaviour cb, float amnt) {
        float timer = slowTime;
        FaceTarget(cb);
        while (timer > 0) {
            timer -= Time.deltaTime;
            yield return null;
        }

        var count = EffectStacks.GetCreateEffect(cb, EffectString(), 0);
        EffectStacks.DecEffectCount(cb, EffectString());

        if (count <= maxStack) {
            cb?.movement.ModifySpeed(1f / amnt); // to remove, apply inverse modification
        }
    }

    string EffectString() {
        return $"{GetHashCode()}_{slow}_{slowTime}_COUNT";
    }


    // tracks how many stacks of each effect is applied to each creep
    static class EffectStacks {
        static Dictionary<CreepBehaviour, Dictionary<string, int>> appliedEffects 
            = new Dictionary<CreepBehaviour, Dictionary<string, int>>();

        // pool for efficiency
        static LPE.ObjectPool<Dictionary<string, int>> innerPool =
            new LPE.ObjectPool<Dictionary<string, int>>(() => new Dictionary<string, int>());


        static Dictionary<string, int> GetCreateEffects(CreepBehaviour c) {
            if (!appliedEffects.ContainsKey(c)) {
                appliedEffects.Add(c, innerPool.Get());
            }

            return appliedEffects[c];
        }

        static float GetCreateEffect(Dictionary<string, int> effects, string effect, int init) {
            if (!effects.ContainsKey(effect)) {
                effects.Add(effect, init);
                return init;
            }

            return effects[effect];
        }

        public static float GetCreateEffect(CreepBehaviour c, string effect, int init) {
            return GetCreateEffect(GetCreateEffects(c), effect, init);
        }

        public static void IncEffectCount(CreepBehaviour c, string effect) {
            appliedEffects[c][effect]++;
        }

        public static void DecEffectCount(CreepBehaviour c, string effect) {
            appliedEffects[c][effect]--;
            // clear empty entries
            if (appliedEffects[c][effect] <= 0) {
                appliedEffects[c].Remove(effect);

                if (appliedEffects[c].Count == 0) {
                    innerPool.Return(appliedEffects[c]);
                    appliedEffects.Remove(c);
                }
            }

        }
    }

}
