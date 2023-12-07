using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonCharacter : MonoBehaviour
{
    [SerializeField] private DeathHandleSystem deathHandleSystem;

    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Transform Ant1;
    [SerializeField] private Transform Ant2;
    [SerializeField] private Transform Ant3;

    [SerializeField] private TextMeshProUGUI foodValue;
    [SerializeField] private int Food;

    private void Start()
    {
        foodValue.text = Food+"";
        deathHandleSystem.DeathHappen += OnFoodValueChange;
    }

    public void SummonAnt1()
    {
        if (Food >= 10)
        {
            Instantiate(Ant1, SpawnPoint.position, Quaternion.identity);
            Food -= 10;
            foodValue.text = Food + "";
        }
    }

    public void SummonAnt2()
    {
        if (Food >= 40)
        {
            Instantiate(Ant2, SpawnPoint.position, Quaternion.identity);
            Food -= 40;
            foodValue.text = Food + "";
        }
    }

    public void SummonAnt3()
    {
        if (Food >= 25)
        {
            Instantiate(Ant3, SpawnPoint.position, Quaternion.identity);
            Food -= 25;
            foodValue.text = Food + "";
        }
    }

    public int GetFoodValue()
    {
        return Food;
    }

    /*public void AddFoodValue(int amount)
    {
        Food += amount;
    }*/

    public void OnFoodValueChange(object sender, FoodValueArgs e) //±µ¦¬
    {
        //Debug.Log("An ant died");
        if (e.antType == "Enemy")
        {
            Food += e.foodValue;
            foodValue.text = Food + "";
        }
    }
}

public class FoodValueArgs : System.EventArgs
{
    public int foodValue;
    public string antType;
    public FoodValueArgs(int foodValue, string antType) //constructor
    {
        this.foodValue = foodValue;
        this.antType = antType;
    }
}
