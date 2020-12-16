using UnityEngine;
using UnityEngine.UI;

public class SimpleEconomyManager : MonoBehaviour {
    public int money { get; private set; }
    public int health { get; private set; }
    public int startMoney = 100;
    public int startHealth = 100;
    public Text moneyText;
    public Text healthText;

    /// <summary>
    /// params: (prev, current)
    /// </summary>
    public event System.Action<int, int> OnMoneyChanged;
    /// <summary>
    /// params: (prev, current)
    /// </summary>
    public event System.Action<int, int> OnHealthChanged;


    public void Init(int money, int health) {
        this.money = startMoney;
        this.health = startHealth;
        OnMoneyChanged += (_, x) => moneyText.text = x.ToString();
        OnHealthChanged += (_, x) => healthText.text = x.ToString();

        moneyText.text = money.ToString();
        healthText.text = health.ToString();
    }

    public bool TrySpend (int amnt) {
        if (money < amnt) {
            return false;
        }

        money -= amnt;

        OnMoneyChanged?.Invoke(money + amnt, money);
        return true;
    }

    public bool EconCheck(int amnt)
    {
        if (money < amnt)
        {
            return false;
        }
        return true;
    }

    public void TowerBuyConfirmed(int amnt)
    {
        money -= amnt;

        OnMoneyChanged?.Invoke(money + amnt, money);
    }

    public void DamagePlayer(int amnt) {
        health -= amnt;

        OnHealthChanged?.Invoke(health + amnt, health);
    }
    public void AddMoney(int amnt) {
        money += amnt;

        OnMoneyChanged?.Invoke(money - amnt, money);
    }
}