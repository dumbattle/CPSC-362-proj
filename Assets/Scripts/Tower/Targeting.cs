using UnityEngine;
using System.Linq;


public enum Targeting {
    first,
    random,
    closest,
    last
}

public static class TargetingUtility {
    public static CreepBehaviour GetTarget(this Targeting e, Vector2 position, float radius) {
        switch (e) {
            case Targeting.first:
                return First(position,radius);
            case Targeting.random:
                return Random(position,radius);
            case Targeting.closest:
                return Closest(position,radius);
            case Targeting.last:
                return Last(position,radius);
            default:
                return null;
        }
    }

    public static CreepBehaviour First(Vector2 position, float radius) {
        var enemies = CreepManager.main.AllCreeps();

        float dte = Mathf.Infinity;
        CreepBehaviour result = null;

        foreach (var enemy in enemies) {
            if (enemy.health.current < 0) {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);
            if (distanceToEnemy <= radius && enemy.movement.distanceToEnd < dte) {
                dte = enemy.movement.distanceToEnd;
                result = enemy;
            }
        }

        return result;
    }
    public static CreepBehaviour Random(Vector2 position, float radius) {
        var enemies = CreepManager.main.AllCreeps();

        float r = 0;
        CreepBehaviour result = null;

        foreach (var enemy in enemies) {
            if (enemy.health.current < 0) {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);

            if (distanceToEnemy <= radius) {
                r++;

                if (UnityEngine.Random.value < 1f / r) {
                    result = enemy;
                }
            }
        }

        return result;
    }
    public static CreepBehaviour Closest(Vector2 position, float radius) {
        var enemies = CreepManager.main.AllCreeps();

        float shortestDistance = Mathf.Infinity;
        CreepBehaviour result = null;

        foreach (var enemy in enemies) {
            if (enemy.health.current < 0) {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);

            if (distanceToEnemy <= radius && distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                result = enemy;
            }
        }

        return result;
    }
    public static CreepBehaviour Last(Vector2 position, float radius) {
        var enemies = CreepManager.main.AllCreeps();

        float dte = Mathf.NegativeInfinity;
        CreepBehaviour result = null;

        foreach (var enemy in enemies) {
            if (enemy.health.current < 0) {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);

            if (distanceToEnemy <= radius && enemy.movement.distanceToEnd > dte) {
                dte = enemy.movement.distanceToEnd;
                result = enemy;
            }
        }

        return result;
    }
}
