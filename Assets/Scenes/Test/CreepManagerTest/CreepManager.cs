using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    List<CreepBehaviour> creepList = new List<CreepBehaviour>();  // list of creeps
    List<CreepBehaviour> creepToRemove = new List<CreepBehaviour>();
    public int creepCount => creepList.Count; 

    public IEnumerable<CreepBehaviour> AllCreeps()
    {
       /* for (int i = 0; i < creepList.Count; i++)
        {
            var t = creepList[i];
            if (creepList != null)
                yield return t;
        }
        */
       return creepList;   // return all creeps
    }

    public CreepBehaviour SpawnCreep(CreepBehaviour src)
    {
        var spawn = Instantiate(src);
        spawn.gameObject.SetActive(true);
        creepList.Add(spawn);
        spawn.Init();
        spawn.OnReachedEnd += () => RemoveCreep(spawn);
        return spawn;
    }

    public void RemoveCreep(CreepBehaviour creep)
    {
        creepToRemove.Add(creep);
    }
    
    public void ClearAll()
    {
        creepList.Clear();
    }

    public void GameplayUpdate()
    {
        foreach (var c in creepToRemove) {
            creepList.Remove(c);
        }

        foreach (var t in creepList) {
            if (t == null) {
                continue;
            }
            t.GameplayUpdate();
        }

        foreach (var c in creepToRemove) {
            creepList.Remove(c);
        }
    }
}


