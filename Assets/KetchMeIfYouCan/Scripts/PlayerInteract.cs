using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float m_MaxInteractDistance = 50.0f;
    public List<GameObject> m_StolenObjects;
    //public bool m_StealingNow = false;
    //public GameObject m_StolenItem;

    private void Awake()
    {
        m_StolenObjects = new List<GameObject>();
    }

    private void Update()
    {
        Steal();
    }

    private void Steal()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, m_MaxInteractDistance))
            {
                if (hit.transform.gameObject.CompareTag("Stealable") || hit.transform.gameObject.CompareTag("GoalItem"))
                {
                    var pickedUpItem = hit.transform.gameObject;
                    pickedUpItem.SetActive(false);
                    Debug.Log("Stole " + hit.transform.gameObject.name);

                    if (pickedUpItem.tag == "GoalItem")
                    {
                        m_StolenObjects.Add(pickedUpItem);
                    }
                }
            }
        }
    }
}
