using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    public Text m_HUDText;

    public List<GameObject> m_StealablePaintings = new List<GameObject>();
    public List<GameObject> m_StealableDisplayItems = new List<GameObject>();
    public List<GameObject> m_DisplayCases = new List<GameObject>();
    public List<GameObject> m_PaintingFrames = new List<GameObject>();

    public GameObject[] m_ObjectiveItems;
    public int m_NumOfObjectiveItems = 3;

    public FirstPersonController m_FPSController;

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

        //Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SetupGame();
    }

	public static void DisplayTextHUD(string message, float length)
    {
        m_Instance.StopCoroutine("DisplayTextHUDDelay");
        m_Instance.m_HUDText.text = "";       
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

        yield return new WaitForSeconds(2.0F);

        m_Instance.m_HUDText.text = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    private void SetupGame()
    {
        //Clear HUD Text
        m_Instance.m_HUDText.text = "";

        //Find all paintings in level
        m_Instance.m_StealablePaintings = new List<GameObject>(GameObject.FindGameObjectsWithTag("StealablePainting"));
        //Debug.Log("GameManager found " + m_Instance.m_StealablePaintings.Count + " paintings.");

        //Find all painting frames in level
        m_Instance.m_PaintingFrames = new List<GameObject>(GameObject.FindGameObjectsWithTag("PaintingSpawn"));
        //Debug.Log("GameManager found " + m_Instance.m_PaintingFrames.Count + " painting frames.");
        RandomizeGOList(m_Instance.m_PaintingFrames);

        //Find all display case items in level
        m_Instance.m_StealableDisplayItems = new List<GameObject>(GameObject.FindGameObjectsWithTag("StealableDisplayItem"));
        //Debug.Log("GameManager found " + m_Instance.m_StealableDisplayItems.Count + " display case items.");

        //Find all display cases in level
        m_Instance.m_DisplayCases = new List<GameObject>(GameObject.FindGameObjectsWithTag("DisplaySpawn"));
        //Debug.Log("GameManager found " + m_Instance.m_DisplayCases.Count + " display cases.");
        RandomizeGOList(m_Instance.m_DisplayCases);

        ChooseObjectiveItems(m_NumOfObjectiveItems);

        //Spawn paintings in frames
        for (int i = 0; i < m_Instance.m_PaintingFrames.Count; i++)
        {
            if (m_Instance.m_StealablePaintings.Count <= 0)
            {
                continue;
            }
            var randomIndex = Random.Range(0, m_Instance.m_StealablePaintings.Count);
            m_Instance.m_StealablePaintings[randomIndex].transform.position = m_Instance.m_PaintingFrames[i].transform.position;
            m_Instance.m_StealablePaintings[randomIndex].transform.rotation = m_Instance.m_PaintingFrames[i].transform.rotation;
            m_Instance.m_StealablePaintings.RemoveAt(randomIndex);
        }

        //Spawn display items in cases
        for (int i = 0; i < m_Instance.m_DisplayCases.Count; i++)
        {
            if (m_Instance.m_StealableDisplayItems.Count <= 0)
            {
                continue;
            }
            var randomIndex = Random.Range(0, m_Instance.m_StealableDisplayItems.Count);
            m_Instance.m_StealableDisplayItems[randomIndex].transform.position = m_Instance.m_DisplayCases[i].transform.position;
            m_Instance.m_StealableDisplayItems[randomIndex].transform.rotation = m_Instance.m_DisplayCases[i].transform.rotation;
            m_Instance.m_StealableDisplayItems.RemoveAt(randomIndex);
        }
    }

    private void ChooseObjectiveItems(int numItemsToSteal)
    {
        if (m_Instance.m_StealableDisplayItems.Count + m_Instance.m_StealablePaintings.Count < numItemsToSteal)
        {
            Debug.LogWarning("WARNING: There are not enough items to steal.");
            return;
        }

        m_Instance.m_ObjectiveItems = new GameObject[numItemsToSteal];

        var numPaintingsToSteal = Random.Range(1, numItemsToSteal);
        var numDisplayItemsToSteal = numItemsToSteal - numPaintingsToSteal;

        //Tag objective paintings
        for (int i = 0; i < numPaintingsToSteal;)
        {
            var randomIndex = Random.Range(0, m_Instance.m_StealablePaintings.Count);
            if (m_Instance.m_StealablePaintings[randomIndex].tag == "ObjectiveItem")
            {
                continue;
            }
            else
            {
                m_Instance.m_StealablePaintings[randomIndex].tag = "ObjectiveItem";
                m_Instance.m_ObjectiveItems[i] = m_Instance.m_StealablePaintings[randomIndex];
                i++;
            }
        }

        //Tag objective display items
        for (int i = 0; i < numDisplayItemsToSteal;)
        {
            var randomIndex = Random.Range(0, m_Instance.m_StealableDisplayItems.Count);
            if (m_Instance.m_StealableDisplayItems[randomIndex].tag == "ObjectiveItem")
            {
                continue;
            }
            else
            {
                m_Instance.m_StealableDisplayItems[randomIndex].tag = "ObjectiveItem";
                m_Instance.m_ObjectiveItems[i + numPaintingsToSteal] = m_Instance.m_StealableDisplayItems[randomIndex];
                i++;
            }
        }
        
        //List objective items
        foreach (var item in m_Instance.m_ObjectiveItems)
        {
            Debug.Log(item + " is an objective item.");
        }
    }

    private void RandomizeGOList(List<GameObject> listToRandomize)
    {
        for (int i = 0; i < listToRandomize.Count; i++)
        {
            var temp = listToRandomize[i];
            var randomIndex = Random.Range(i, listToRandomize.Count);
            listToRandomize[i] = listToRandomize[randomIndex];
            listToRandomize[randomIndex] = temp;
        }
    }
}
