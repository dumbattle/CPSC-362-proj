using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviour : MonoBehaviour, ITower {
    protected TowerBehaviour src;
    public TowerManager tm;
    public SimpleEconomyManager em;

    public Vector2Int mapIndex { get; private set; }
    public static TowerBehaviour towerUpgrade { get; private set; }

    [Header("Tower 1 Upgrade Objects")]
    public TowerBehaviour Tower1_2_Upgrade;
    public TowerBehaviour Tower1_3_Upgrade;
    [Header("Tower 2 Upgrade Objects")]
    public TowerBehaviour Tower2_2_Upgrade;
    public TowerBehaviour Tower2_3_Upgrade;
    [Header("Tower 3 Upgrade Objects")]
    public TowerBehaviour Tower3_2_Upgrade;
    public TowerBehaviour Tower3_3_Upgrade;

    public int cost;
    int ITower.cost => cost;
    public int level;
    int ITower.level => level;
    public int type;
    int ITower.type => type;

    public abstract void GameplayUpdate();
    public abstract void WaitUpdate();
    public virtual void Init(Vector2Int index, TowerBehaviour src) { mapIndex = index; this.src = src; }

    protected void FaceTarget(CreepBehaviour cb) {
        var dir = cb.transform.position - transform.position;
        // set forward axis to point to creep
        transform.up = dir;
    }

    void ITower.DestroyTower() {
        Destroy(gameObject);
    }

    ITower ITower.UpgradeTower(int x, int y)
    {
        var t = tm.GetTower(x, y);
        ITower u = null;

        if (t.level != 3)
        {
            Debug.Log(t.type + " " + t.level);
            switch (t.type)
            {
                case 1:
                    if (t.level == 1)
                    {
                        if (em.TrySpend(Tower1_2_Upgrade.cost))
                        {
                            tm.RemoveTower(x, y);
                            u = tm.CreateTower(Tower1_2_Upgrade, x, y);
                        }
                    }
                    else if (t.level == 2)
                    {
                        if (em.TrySpend(Tower1_3_Upgrade.cost))
                        {
                            tm.RemoveTower(x, y);
                            u = tm.CreateTower(Tower1_3_Upgrade, x, y);
                        }
                    }
                    return u;

                case 2:
                    if (t.level == 1)
                    {
                        if (em.TrySpend(Tower2_2_Upgrade.cost))
                        {
                            tm.RemoveTower(x, y);
                            u = tm.CreateTower(Tower2_2_Upgrade, x, y);
                        }
                    }
                    else if (t.level == 2)
                    {
                        if (em.TrySpend(Tower2_3_Upgrade.cost))
                        {
                            tm.RemoveTower(x, y);
                            u = tm.CreateTower(Tower2_3_Upgrade, x, y);
                        }
                    }
                    return u;


                case 3:
                    if (t.level == 1)
                    {
                        if (em.TrySpend(Tower3_2_Upgrade.cost))
                        {
                            tm.RemoveTower(x, y);
                            u = tm.CreateTower(Tower3_2_Upgrade, x, y);
                        }
                    }
                    else if (t.level == 2)
                    {
                        if (em.TrySpend(Tower3_3_Upgrade.cost))
                        {
                            tm.RemoveTower(x, y);
                            u = tm.CreateTower(Tower3_3_Upgrade, x, y);
                        }
                    }
                    return u;

                default:
                    return u;
            }
        }
        else
            return u;
    }
}
