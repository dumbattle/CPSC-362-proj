using UnityEngine;

public class PathManager : MonoBehaviour {
    public static CreepPath GetRandomPath() {
        return _main.RandomPath();
    }
    static PathManager _main;


    

    public CreepPath[] paths;
    int i = 0;

    private void Awake() {
        _main = this;
    }

    public CreepPath RandomPath() {
        i++;
        i %= paths.Length;
        return paths[i];
    }
}
