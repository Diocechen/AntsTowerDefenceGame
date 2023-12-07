using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandleSystem : MonoBehaviour
{
    static public WinLoseHandleSystem instance; //singleton

    [SerializeField] private WinLoseScreen winLoseScreen;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver(bool win)
    {
        winLoseScreen.SetUp(win);
    }
}
