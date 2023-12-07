using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCharacterEnemy : MonoBehaviour
{
    [SerializeField] private DeathHandleSystem deathHandleSystem;

    [SerializeField] private GameObject[] antPrefabs; // ���ƹw�s���}�C
    [SerializeField] private Transform spawnPoint; // �۳��I��m
    [SerializeField] private float[] cooldownTimes; // �U���ƧN�o�ɶ�
    [SerializeField] private int[] foodConsume; //���ӭ���
    [SerializeField] private int Food;

    private bool isCooldown = false; // �O�_���b�N�o��

    // �}�l�ɩI�s
    private void Start()
    {
        deathHandleSystem.DeathHappen += OnFoodValueChange;
        StartCoroutine(SpawnAntsCoroutine());
    }

    // ��{: �۰ʩ۳����
    private IEnumerator SpawnAntsCoroutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                // �H����ܤ@�ؿ���
                int antIndex = Random.Range(0, antPrefabs.Length);
                GameObject antPrefab = antPrefabs[antIndex];

                if (Food >= foodConsume[antIndex])
                {
                    // �۳����
                    Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

                    // �ҰʧN�o�p��
                    isCooldown = true;
                    StartCoroutine(CooldownCoroutine(cooldownTimes[antIndex]));

                    Food -= foodConsume[antIndex];
                }
            }

            yield return null;
        }
    }

    // ��{: �N�o�p��
    private IEnumerator CooldownCoroutine(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    public void OnFoodValueChange(object sender, FoodValueArgs e) //����
    {
        //Debug.Log("An ant died");
        if (e.antType == "Friend")
        {
            Food += e.foodValue;
        }
    }
}
