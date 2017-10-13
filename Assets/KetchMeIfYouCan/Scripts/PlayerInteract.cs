using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public float m_MaxInteractDistance = 50.0f;
    public float m_InteractTimeAmount = 1.5f;
    public float m_CurrentInteractTime = 0.0f;
    public Image m_StealProgressBar;
    public List<GameObject> m_StolenObjectiveItems;
    public GameObject m_StolenItem;
    public GameObject inventoryMgr;
    [SerializeField]
    private Inventory inventoryList;

    //sound variables
    [SerializeField] AudioSource myAudio;
	public AudioClip stealPaintingClip;
	public AudioClip stealCaseClip;

	private void Awake()
    {
        //m_playerController = FindObjectOfType(typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController)) as UnityStandardAssets.Characters.FirstPerson.FirstPersonController;
        m_StolenObjectiveItems = new List<GameObject>();
    }

    private void Update()
    {
        Steal();

        if (Tooltip.Instance != null) {
            Tooltip.Instance.CheckForTooltip(transform.position + transform.forward, transform.forward, m_MaxInteractDistance);
        }
    }

    private void Steal()
    {
        if (Input.GetButton("Interact"))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, m_MaxInteractDistance))
            {
                if (hit.transform.gameObject.CompareTag("ObjectiveItem"))
                {
                    m_StealProgressBar.fillAmount += Time.deltaTime / m_InteractTimeAmount;

                    if (m_StealProgressBar.fillAmount >= m_InteractTimeAmount / m_InteractTimeAmount)
                    {
                        //Assign object being stolen
                        var pickedUpItem = hit.transform.gameObject;

                        m_StolenObjectiveItems.Add(pickedUpItem);

                        //Disable object being stolen
                        pickedUpItem.SetActive(false);

                        //remove objective from UI target list
                        inventoryMgr.GetComponent<InventoryMgr>().RemoveObjective(pickedUpItem);
                        //update UI if open
                        inventoryMgr.GetComponent<InventoryMgr>().ClearBufferInventory();
                        inventoryMgr.GetComponent<InventoryMgr>().PopulateInventory(inventoryList);
                        
                        ClearHUDText();

                        //sound for item stolen
                        /*//Need to assign audio clip to audio source (the audio source will be the stolen object) but can't do this until stealing mechanics work to test, currenctly they don't.
                        if (m_StolenItem.tag == "StealablePainting" || m_StolenItem.tag == "StealableDisplayItem")
                        {
                            myAudio.clip = stealPaintingClip;
                        }
                        myAudio.Play();*/
                    }

                }
                else if (hit.transform.gameObject.CompareTag("StealablePainting") || hit.transform.gameObject.CompareTag("StealableDisplayItem"))
                {
                    m_StealProgressBar.fillAmount = 0.0f;
                    GameManager.DisplayTextHUD("This isn't an objective item.", 2.0f);
                }
            }
            else
            {
                ClearHUDText();
            }
        }

        if (Input.GetButtonUp("Interact"))
        {
            ClearHUDText();
        }
    }

    private void ClearHUDText()
    {
        m_StealProgressBar.fillAmount = 0.0f;
        GameManager.m_Instance.m_HUDText.text = "";
    }
}
