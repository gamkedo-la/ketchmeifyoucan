using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckGameConditions : MonoBehaviour
{
    public PlayerInteract m_playerInteract;
    public int m_MinLootItems = 3;
    public Text m_HUDText;

    private void OnTriggerEnter(Collider other)
    {
        CheckLoot();
    }

    private void CheckLoot()
    {
        if (m_playerInteract.m_StolenObjectiveItems.Count >= GameManager.m_Instance.m_NumOfObjectiveItems)
        {
            GameManager.RestartGame("YOU WIN!");
        }
        else
        {
            GameManager.DisplayTextHUD("YOU ARE MISSING " + (GameManager.m_Instance.m_NumOfObjectiveItems - m_playerInteract.m_StolenObjectiveItems.Count).ToString() + " ITEMS.", 3.0f);
        }
    }
}
