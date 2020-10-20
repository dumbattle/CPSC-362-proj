using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
     List<CreepBehaviour> creepList = new List<CreepBehaviour>();  // list of creeps
     
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
        return src;
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
        foreach (var t in creepList)
            t.GameplayUpdate();
    }
}


public struct CreepBehavior
{
    public GameObject refr;

    public CreepBehavior(GameObject obj)
    {
        this.refr = obj;
    }
}
