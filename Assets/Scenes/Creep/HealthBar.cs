using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

     private Transform bar;
    // Start is called before the first frame update
    void Start()
    {
          Transform bar = transform.find("Bar");
    }

     public void SetSize(float sizeNormal)
     {

          bar.localScale = new Vector3(sizeNormal, 1f);
     }
}
