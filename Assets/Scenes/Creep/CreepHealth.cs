using UnityEngine;


public class CreepHealth : MonoBehaviour {
    public int max { get; private set; }
    public int current { get; private set; }
    public event System.Action OnHealthChange;

    public void Init(int maxHP) {
        max = current = maxHP;
    }

     
     public void damage(int amount)
     {
          current-=amount;
          OnHealthChange?.Invoke();
     }

      


     // TODO - add functionality
}
