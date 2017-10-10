using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckGameConditions : MonoBehaviour
{
    public PlayerInteract m_PlayerInteract;
    public Text m_HUDText;
    public stopwatchGUI m_StopWatch;

    private void OnTriggerEnter(Collider other)
    {
        CheckLoot();
    }

    private void CheckLoot()
    {
        if (m_PlayerInteract.m_StolenObjectiveItems.Count >= GameManager.m_Instance.m_NumOfObjectiveItems)
        {
            var totalSeconds = Mathf.Round(m_StopWatch.totalTime);
            var totalTime = "";

            if (totalSeconds < 60)
            {
                totalTime = totalSeconds.ToString() + " seconds";
            }
            else
            {
                var minutes = Mathf.Round(totalSeconds / 60);
                var seconds = Mathf.Round(totalSeconds % 60);
                if (minutes > 1)
                {
                    totalTime = minutes + " minutes and " + seconds + " seconds";
                }
                else
                {
                    totalTime = minutes + " minute and " + seconds + " seconds";
                }
            }

            GameManager.RestartGame("Congratulations! It took you " + totalTime + " to rob the museum!");
        }
        else
        {
            GameManager.DisplayTextHUD("YOU ARE MISSING " + (GameManager.m_Instance.m_NumOfObjectiveItems - m_PlayerInteract.m_StolenObjectiveItems.Count).ToString() + " ITEMS.", 3.0f);
        }
    }
}
