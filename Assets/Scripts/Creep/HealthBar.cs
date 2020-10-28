using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
     public CreepHealth health;
     public Transform bar;
    // Start is called before the first frame update
    void Start()
    {
          Transform bar = transform.Find("Bar");
          health.OnHealthChange += () => SetSize((float)health.current/health.max);
    }

     public void SetSize(float sizeNormal)
     {

          bar.localScale = new Vector3(sizeNormal, 1f);
     }
}
