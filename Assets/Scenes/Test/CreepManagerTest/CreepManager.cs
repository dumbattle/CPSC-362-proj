using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    List<CreepBehaviour> creepList = new List<CreepBehaviour>();  // list of creeps
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
        return spawn;
    }

    public void RemoveCreep(CreepBehaviour creep)
    {
        creepList.Remove(creep);
    }
    
    public void ClearAll()
    {
        creepList.Clear();
    }

    public void GameplayUpdate()
    {
        //Extremely inefficient way to clear null(dead) creeps
        //TODO: replace with a better method
        creepList = (from x in creepList where x != null select x).ToList();

        foreach (var t in creepList) {
            t.GameplayUpdate();
        }
    }
}


