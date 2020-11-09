using UnityEngine;

public class CreepArmorComponent : CreepCommponent {
    [Min(0)]
    public int amnt;

    public override void Init(CreepBehaviour cb) {
        cb.health.damageStep.OnPreprocessRawDMG += (r,c) =>  Mathf.Max(1, r - amnt);
    }
}