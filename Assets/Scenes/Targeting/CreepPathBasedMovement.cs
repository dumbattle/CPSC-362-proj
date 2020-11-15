using UnityEngine;

public class CreepPathBasedMovement : CreepMovement {
    [SerializeField]
    float _baseSpeed = 2f;

    float speedMod = 1;
    public float baseSpeed => _baseSpeed;

    public float speed => _baseSpeed * speedMod;



    CreepPath path;
    float position = 0;
    Vector2 offset;


    public override void Init() {
        path = PathManager.GetRandomPath();
        SetPosition();
        offset = Random.insideUnitCircle * .3f;
    }
    public override void GameplayUpdate() {
        position += Time.deltaTime * speed;
        SetPosition();
    }


    public override bool ModifySpeed(float amnt) {
        if (amnt < 0) {
            return false;
        }

        speedMod *= amnt;
        return true;
    }

    void SetPosition() {
        distanceToEnd = path.path.Count - 1f - position;
        distanceTraveled = position;
        int a = (int)position;
        int b = a + 1;

        if (a == path.path.Count - 1) {
            CallOnReachedEnd();
            return;
        }

        transform.position = Vector2.Lerp(path.path[a], path.path[b], position % 1) + offset;
    }
}