using UnityEngine;


public class CreepHealth : MonoBehaviour {
    [SerializeField]
    int startingHealth = 1;

    public int max { get; private set; }
    public int current { get; private set; }
    public event System.Action OnHealthChange;

    public void Init() {
        max = current = startingHealth;
    }

     
     public void damage(int amount)
     {
          current-=amount;
          OnHealthChange?.Invoke();
     }
}
