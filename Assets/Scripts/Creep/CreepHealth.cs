using UnityEngine;
using System;

public class CreepHealth : MonoBehaviour {
    [SerializeField]
    int startingHealth = 1;

    public int max { get; private set; }
    public int current { get; private set; }
    public event Action OnHealthChange;
    public DamageStep damageStep;
    public void Init() {
        max = current = startingHealth;
        damageStep = DamageStep.Get();
    }


    public void damage(int amount) {
        var dmg = damageStep.ProcessDMG(amount);
        current -= dmg;

        damageStep.DamageDealt(dmg);

        OnHealthChange?.Invoke();
    }

    public int Heal(int amnt) {
        current += amnt;

        if (current > max) {
            var excess = current - max;
            current = max;
            amnt -= excess;
        }

        OnHealthChange?.Invoke();
        return amnt;
    }

    public void Reset() {
        DamageStep.Return(damageStep);
    }
}

public class DamageStep {
    static LPE.ObjectPool<DamageStep> _pool = new LPE.ObjectPool<DamageStep>(() => new DamageStep());

    public static DamageStep Get() => _pool.Get();
    public static void Return(DamageStep ds) => _pool.Return(ds);

    private DamageStep() { }

    /// <summary>
    /// int Dmg(int raw, int current)
    /// </summary>
    public event Func<int, int, int> OnPreprocessRawDMG;
    public event Action<int> OnPreprocessDMG;
    public event Action<int> OnProcessDealtDMG;


    public int ProcessDMG(int rawDmg) {
        var dmg = rawDmg;
        if (OnPreprocessRawDMG != null) {
            foreach (var f in OnPreprocessRawDMG.GetInvocationList()) {
                dmg = (int)f.DynamicInvoke(rawDmg, dmg);
            }
        }
        OnPreprocessDMG?.Invoke(dmg);

        return dmg;
    }

    public void DamageDealt(int amnt) {
        OnProcessDealtDMG?.Invoke(amnt);
    }
}
