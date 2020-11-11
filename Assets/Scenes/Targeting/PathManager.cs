using UnityEngine;

public class PathManager : MonoBehaviour {
    public static CreepPath GetRandomPath() {
        return _main.RandomPath();
    }
    static PathManager _main;

    public CreepPath[] paths;

    private void Awake() {
        _main = this;
    }

    public CreepPath RandomPath() {
        return paths[Random.Range(0, paths.Length)];
    }
}
