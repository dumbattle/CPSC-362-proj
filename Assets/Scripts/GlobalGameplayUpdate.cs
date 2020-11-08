using System.Collections;
using System.Collections.Generic;

// TODO - refactor 
// this class is a sudo-gamemanger
public static class GlobalGameplayUpdate {
    static LinkedList<IEnumerator> _gameplayUpdates = new LinkedList<IEnumerator>();
    static LinkedList<IEnumerator> _waitUpdates = new LinkedList<IEnumerator>();

    static List<IEnumerator> _gameplayUpdates_TOREMOVE = new List<IEnumerator>();
    static List<IEnumerator> _waitUpdates_TOREMOVE = new List<IEnumerator>();


    public static void AddGameplayUpdate(IEnumerator func) {
        _gameplayUpdates.AddLast(func);
    }
    public static void AddWaitUpdate(IEnumerator func) {
        _waitUpdates.AddLast(func);
    }

    /// <summary>
    /// If the func is completed in one of the updates, it will also be removed from the other immediatly
    /// </summary>
    public static void AddGameplayWaitUpdate(IEnumerator func) {
        // store both updates in this array so they can reference each other
        IEnumerator[] iea = new IEnumerator[2];
        iea[0] = gp(iea);
        iea[1] = w(iea);

        AddGameplayUpdate(iea[0]);
        AddWaitUpdate(iea[1]);

        IEnumerator gp(IEnumerator[] a) {
            while(func.MoveNext()) {
                yield return null;
            }
            RemoveWaitUpdate(iea[1]);
        }

        IEnumerator w(IEnumerator[] a) {
            while (func.MoveNext()) {
                yield return null;
            }
            RemoveGameplayUpdate(iea[0]);

        }
    }

    // removal will happen the next frame
    public static void RemoveGameplayUpdate(IEnumerator func) {
        _gameplayUpdates_TOREMOVE.Add(func);
    }
    // removal will happen the next frame
    public static void RemoveWaitUpdate(IEnumerator func) {
        _waitUpdates_TOREMOVE.Add(func);
    }

    public static void GameplayUpdate() {
        RemoveAll();
        var n = _gameplayUpdates.First;

        while (n != null) {
            var next = n.Next;

            if (!n.Value.MoveNext()) {
                _gameplayUpdates.Remove(n);
            }

            n = next;
        }
        RemoveAll();
    }

    public static void WaitUpdate() {
        RemoveAll();
        var n = _waitUpdates.First;

        while (n != null) {
            var next = n.Next;

            if (!n.Value.MoveNext()) {
                _waitUpdates.Remove(n);
            }

            n = next;
        }
        RemoveAll();
    }

    static void RemoveAll() {
        RemoveGPUs();
        RemoveWUs();
    }

    static void RemoveGPUs() {
        for (int i = 0; i < _gameplayUpdates_TOREMOVE.Count; i++) {
            _gameplayUpdates.Remove(_gameplayUpdates_TOREMOVE[i]);
        }
        _gameplayUpdates_TOREMOVE.Clear();
    }

    static void RemoveWUs() {
        for (int i = 0; i < _waitUpdates_TOREMOVE.Count; i++) {
            _waitUpdates.Remove(_waitUpdates_TOREMOVE[i]);
        }
        _waitUpdates_TOREMOVE.Clear();
    }


}
