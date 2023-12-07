using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownButton : MonoBehaviour
{
    [SerializeField] private float countdownTime = 5f; // �˼ƭp�ɪ��ɶ��]�H�����^
    [SerializeField] private Button button; // ���s

    private bool isCountingDown = false; // �O�_���b�p�ɤ�

    // ����s�Q�I���ɩI�s
    public void OnButtonClick()
    {
        if (!isCountingDown)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    // ��{: �˼ƭp��
    private IEnumerator CountdownCoroutine()
    {
        isCountingDown = true;
        button.interactable = false; // �T�Ϋ��s

        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        button.interactable = true; // �ҥΫ��s
        isCountingDown = false;
    }
}
