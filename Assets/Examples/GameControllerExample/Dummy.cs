using UnityEngine;


namespace AustinsExamples.GameState {
    // dummy class use to illustrate how to control game entities (creeps, towers
    public class Dummy : MonoBehaviour {
        public SpriteRenderer sr;
        public float speed;

        Vector3 dest;

        // don't use Start() or Awake()
        public void Init() {
            dest = Random.insideUnitCircle * 5;
            sr = GetComponent<SpriteRenderer>();
        }

        // don't use Update()
        public void GameplayUpdate() {
            var dir = dest - transform.position;

            if (dir.sqrMagnitude < .1f) {
                dest = Random.insideUnitCircle * 5;
                GameplayUpdate();
                return;
            }

            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}