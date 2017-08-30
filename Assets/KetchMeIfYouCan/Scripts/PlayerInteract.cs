using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float m_MaxInteractDistance = 50.0f;
    public List<GameObject> m_StolenObjects;
    public bool m_StealingNow = false;
    public GameObject m_StolenItem;

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_playerController;

    private void Awake()
    {
        m_playerController = FindObjectOfType(typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController)) as UnityStandardAssets.Characters.FirstPerson.FirstPersonController;
        m_StolenObjects = new List<GameObject>();
    }

    private void Update()
    {
        Steal();

        if (Tooltip.Instance != null) {
            Tooltip.Instance.CheckForTooltip(transform.position, transform.forward, m_MaxInteractDistance);
        }
    }

    private void Steal()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, m_MaxInteractDistance))
            {
                if (hit.transform.gameObject.CompareTag("Stealable") || hit.transform.gameObject.CompareTag("ObjectiveItem"))
                {
                    //Assign object being stolen
                    var pickedUpItem = hit.transform.gameObject;

                    //Used by PaintingRemoveAudio script
                    m_StolenItem = pickedUpItem;
                    m_StealingNow = true;

                    //Disable object being stolen
                    pickedUpItem.SetActive(false);

                    //Add item to Stolen Objects if on the Objective list
                    if (pickedUpItem.tag == "ObjectiveItem")
                    {
                        m_StolenObjects.Add(pickedUpItem);
                    }
                }
            }
        }
    }
}
