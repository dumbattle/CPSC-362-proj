﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    List<CreepBehavior> creepList;  // list of creeps
 
    public IEnumerable<CreepBehavior> AllCreeps()
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

    public CreepBehavior SpawnCreep(CreepBehavior src)
    {
        return src;
    }

    public void RemoveCreep(CreepBehavior creep)
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