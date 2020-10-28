using UnityEngine;

public class SimpleCreepGiveMoneyToPlayerWhenKilled : CreepCommponent {
    [Min(0)]
    public int cash;
    public SimpleEconomyManager em;

    public override void Init(CreepBehaviour cb) {
        cb.OnKilled += () => em.AddMoney(cash);
    }
}