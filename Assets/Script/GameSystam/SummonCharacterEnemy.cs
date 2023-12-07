using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCharacterEnemy : MonoBehaviour
{
    [SerializeField] private DeathHandleSystem deathHandleSystem;

    [SerializeField] private GameObject[] antPrefabs; // 螞蟻預製物陣列
    [SerializeField] private Transform spawnPoint; // 招喚點位置
    [SerializeField] private float[] cooldownTimes; // 各螞蟻冷卻時間
    [SerializeField] private int[] foodConsume; //消耗食物
    [SerializeField] private int Food;

    private bool isCooldown = false; // 是否正在冷卻中

    // 開始時呼叫
    private void Start()
    {
        deathHandleSystem.DeathHappen += OnFoodValueChange;
        StartCoroutine(SpawnAntsCoroutine());
    }

    // 協程: 自動招喚螞蟻
    private IEnumerator SpawnAntsCoroutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                // 隨機選擇一種螞蟻
                int antIndex = Random.Range(0, antPrefabs.Length);
                GameObject antPrefab = antPrefabs[antIndex];

                if (Food >= foodConsume[antIndex])
                {
                    // 招喚螞蟻
                    Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

                    // 啟動冷卻計時
                    isCooldown = true;
                    StartCoroutine(CooldownCoroutine(cooldownTimes[antIndex]));

                    Food -= foodConsume[antIndex];
                }
            }

            yield return null;
        }
    }

    // 協程: 冷卻計時
    private IEnumerator CooldownCoroutine(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    public void OnFoodValueChange(object sender, FoodValueArgs e) //接收
    {
        //Debug.Log("An ant died");
        if (e.antType == "Friend")
        {
            Food += e.foodValue;
        }
    }
}
