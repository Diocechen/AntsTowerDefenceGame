using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandleSystem : MonoBehaviour
{
    public event System.EventHandler<FoodValueArgs> DeathHappen;
    private void Update()
    {

    }

    public void AnAntDied(int foodValue, string antType)
    {
        DeathHappen(this, new FoodValueArgs(foodValue, antType));
    }
}
