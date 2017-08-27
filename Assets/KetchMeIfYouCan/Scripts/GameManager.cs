using System.Collections;
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
    }

    private void Start()
    {
		EnumerateStealables();
		ChooseRandomObjects(3);
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
        yield return new WaitForSeconds(3.0f);
        m_Instance.m_HUDText.text = "";
        SceneManager.LoadScene(0);
    }

    private static void ChooseRandomObjects(int numObjToSteal)
    {
		if (m_Instance.m_StealableItems.Count<numObjToSteal)
		{
			Debug.LogWarning("WARNING: ChooseRandomObjects does not have enough m_StealableItems");
			return;
		}

		m_Instance.m_ItemsToSteal = new GameObject[numObjToSteal];

        for (int i = 0; i < numObjToSteal; i++)
        {
            var randomIndex = Random.Range(0, m_Instance.m_StealableItems.Count);
            m_Instance.m_ItemsToSteal[i] = m_Instance.m_StealableItems[randomIndex];
            m_Instance.m_ItemsToSteal[i].tag = "GoalItem";
            m_Instance.m_StealableItems.RemoveAt(randomIndex);
        }

        Debug.Log("Your objective is to steal: " + m_Instance.m_ItemsToSteal[0].name + ", " + m_Instance.m_ItemsToSteal[1].name + ", " + m_Instance.m_ItemsToSteal[2].name);
    }

	private static void EnumerateStealables() // look for any obj with the right TAG
	{
		m_Instance.m_StealableItems = new List<GameObject>(GameObject.FindGameObjectsWithTag("Stealable"));
		Debug.Log("EnumerateStealables found " + m_Instance.m_StealableItems.Count + " stealable objects.");
	}



}
