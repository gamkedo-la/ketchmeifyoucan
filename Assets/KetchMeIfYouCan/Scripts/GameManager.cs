﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    public FirstPersonController m_FPSController;
    public Text m_HUDText;
    public List<GameObject> m_StealableItems = new List<GameObject>();
    public List<GameObject> m_StealableDisplayItems = new List<GameObject>();
    public GameObject[] m_ItemsToSteal;
    public int m_NumberOfItemsToSteal = 3;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
		EnumerateStealables();
        SpawnDisplayItems();
		//ChooseGoalItems(m_NumberOfItemsToSteal);
    }

	public static void DisplayTextHUD(string message, float length)
    {
        m_Instance.StartCoroutine(DisplayTextHUDDelay(message, length));   
    }

    public static IEnumerator DisplayTextHUDDelay(string message, float length)
    {
        m_Instance.m_HUDText.text = message;
        yield return new WaitForSeconds(length);
        m_Instance.m_HUDText.text = "";
    }

    public static void RestartGame(string message)
    {
        m_Instance.StartCoroutine(RestartDelay(message));
    }

    private static IEnumerator RestartDelay(string message)
    {
        m_Instance.m_HUDText.text = message;
        m_Instance.m_FPSController.enabled = false;

        //Screen fade effect.
        FadeEffects.Instance.FadeScreen(1, 0.01f);

        yield return new WaitForSeconds(3.0F);

        m_Instance.m_HUDText.text = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private static void ChooseGoalItems(int numItemsToSteal)
    {
		if (m_Instance.m_StealableItems.Count < numItemsToSteal)
		{
			Debug.LogWarning("WARNING: ChooseRandomObjects does not have enough m_StealableItems");
			return;
		}

		m_Instance.m_ItemsToSteal = new GameObject[numItemsToSteal];

        for (int i = 0; i < numItemsToSteal; i++)
        {
            var randomIndex = Random.Range(0, m_Instance.m_StealableItems.Count);
            m_Instance.m_ItemsToSteal[i] = m_Instance.m_StealableItems[randomIndex];
            m_Instance.m_ItemsToSteal[i].tag = "ObjectiveItem";
            m_Instance.m_StealableItems.RemoveAt(randomIndex);
        }

        Debug.Log("Your objective is to steal: " + m_Instance.m_ItemsToSteal[0].name + ", " + m_Instance.m_ItemsToSteal[1].name + ", " + m_Instance.m_ItemsToSteal[2].name);
    }

	private static void EnumerateStealables() // look for any obj with the right TAG
	{
		m_Instance.m_StealableItems = new List<GameObject>(GameObject.FindGameObjectsWithTag("Stealable"));
		Debug.Log("EnumerateStealables found " + m_Instance.m_StealableItems.Count + " stealable objects.");
	}

    private static void SpawnDisplayItems()
    {
        List<GameObject> spawnableDisplayCaseAreas = new List<GameObject>(GameObject.FindGameObjectsWithTag("DisplaySpawn"));
        for (int i = 0; i < spawnableDisplayCaseAreas.Count; i++)
        {
            var randomStealItemIndex = Random.Range(0, m_Instance.m_StealableDisplayItems.Count);
            Instantiate(m_Instance.m_StealableDisplayItems[randomStealItemIndex], spawnableDisplayCaseAreas[i].transform.position, spawnableDisplayCaseAreas[i].transform.rotation, spawnableDisplayCaseAreas[i].transform.parent);
            m_Instance.m_StealableDisplayItems.RemoveAt(randomStealItemIndex);
        }
    }
}
