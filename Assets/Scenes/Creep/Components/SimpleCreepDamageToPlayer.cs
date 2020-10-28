using UnityEngine;

public class SimpleCreepDamageToPlayer : CreepCommponent {
    [Min(0)]
    public int damage;
    public SimpleEconomyManager em;

    public override void Init(CreepBehaviour cb) {
        cb.OnReachedEnd += () => em.DamagePlayer(damage);
    }
}
