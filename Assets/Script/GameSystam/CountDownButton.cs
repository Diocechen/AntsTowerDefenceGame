using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownButton : MonoBehaviour
{
    [SerializeField] private float countdownTime = 5f; // 倒數計時的時間（以秒為單位）
    [SerializeField] private Button button; // 按鈕

    private bool isCountingDown = false; // 是否正在計時中

    // 當按鈕被點擊時呼叫
    public void OnButtonClick()
    {
        if (!isCountingDown)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    // 協程: 倒數計時
    private IEnumerator CountdownCoroutine()
    {
        isCountingDown = true;
        button.interactable = false; // 禁用按鈕

        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        button.interactable = true; // 啟用按鈕
        isCountingDown = false;
    }
}
