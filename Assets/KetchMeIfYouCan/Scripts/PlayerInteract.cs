using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float m_MaxInteractDistance = 50.0f;
    public List<GameObject> m_StolenObjectiveItems;
    public bool m_StealingNow = false;
    public GameObject m_StolenItem;
	public GameObject inventoryMgr;

	//UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_playerController;

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
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, m_MaxInteractDistance))
            {
                if (hit.transform.gameObject.CompareTag("ObjectiveItem"))
                {
                    //Assign object being stolen
                    var pickedUpItem = hit.transform.gameObject;

                    m_StolenObjectiveItems.Add(pickedUpItem);
					//inventoryMgr.GetComponent<InventoryMgr>().GetItem(pickedUpItem);//TODO ONCE OBJECTS STEAL WHEN PRESSING INTERACT

					//Disable object being stolen
					pickedUpItem.SetActive(false);
                }
                else if (hit.transform.gameObject.CompareTag("StealablePainting") || hit.transform.gameObject.CompareTag("StealableDisplayItem"))
                {
                    GameManager.DisplayTextHUD("This isn't an objective item.", 2.0f);
                }
            }
        }
    }
}
