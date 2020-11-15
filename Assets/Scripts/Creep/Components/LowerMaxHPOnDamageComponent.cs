using UnityEngine;

public class LowerMaxHPOnDamageComponent : CreepCommponent {
    [Range(0,1)]
    public float scale = .5f;
    public override void Init(CreepBehaviour cb) {
        var h = cb.health;
        h.damageStep.OnProcessDealtDMG += (d) => h.SetMaxHP(h.max - (int)(d * scale), false);
    }
}