using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
     public static CreepManager main;
    List<CreepBehaviour> creepList = new List<CreepBehaviour>();  // list of creeps
    List<CreepBehaviour> creepToRemove = new List<CreepBehaviour>();
    public int creepCount => creepList.Count; 

     private void Awake()
     {
          main = this;
     }

    public IEnumerable<CreepBehaviour> AllCreeps()
    {
       return creepList;   // return all creeps
    }


    public CreepBehaviour SpawnCreep(CreepBehaviour src)
    {
        var spawn = Instantiate(src);
        spawn.gameObject.SetActive(true);
        creepList.Add(spawn);
        spawn.Init();
        spawn.OnReachedEnd += () => RemoveCreep(spawn);
        spawn.OnKilled += () => RemoveCreep(spawn);
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

        creepToRemove.Clear();

        foreach (var t in creepList) {
            if (t == null) {
                continue;
            }
            t.GameplayUpdate();
        }

        foreach (var c in creepToRemove) {
            creepList.Remove(c);
        }

        creepToRemove.Clear();
    }
}


