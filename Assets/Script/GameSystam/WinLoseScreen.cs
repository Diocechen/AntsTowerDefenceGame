using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverObj;
    [SerializeField] private TextMeshProUGUI winLoseText;

    public void SetUp(bool win)
    {

        gameOverObj.gameObject.SetActive(true);
        if (win)
        {
            Time.timeScale = 0f;
            winLoseText.text = "YOU ARE WINNER";
        }
        else
        {
            Time.timeScale = 0f;
            winLoseText.text = "YOU ARE LOSER";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
