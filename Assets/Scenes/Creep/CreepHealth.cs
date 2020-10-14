using UnityEngine;


public class CreepHealth : MonoBehaviour {
    public int max { get; private set; }
    public int current { get; private set; }
    public event System.Action OnHealthChange;

    public void Init(int maxHP) {
        max = current = maxHP;
    }


     void Update()
     {
          current--;
          OnHealthChange?.Invoke();
     }

     void Start()
     {
          Init(100);    
     }   


     // TODO - add functionality
}
