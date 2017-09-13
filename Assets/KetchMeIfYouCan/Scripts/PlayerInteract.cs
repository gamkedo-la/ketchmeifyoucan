using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float m_MaxInteractDistance = 50.0f;
    public List<GameObject> m_StolenObjectiveItems;
    public GameObject m_StolenItem;
	public GameObject inventoryMgr;

	//sound variables
	[SerializeField] AudioSource myAudio;
	public AudioClip stealPaintingClip;
	public AudioClip stealCaseClip;

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

					//sound for item stolen
					/*//Need to assign audio clip to audio source (the audio source will be the stolen object) but can't do this until stealing mechanics work to test, currenctly they don't.
					if (m_StolenItem.tag == "StealablePainting" || m_StolenItem.tag == "StealableDisplayItem")
					{
						myAudio.clip = stealPaintingClip;
					}
					myAudio.Play();*/
				}
                else if (hit.transform.gameObject.CompareTag("StealablePainting") || hit.transform.gameObject.CompareTag("StealableDisplayItem"))
                {
                    GameManager.DisplayTextHUD("This isn't an objective item.", 2.0f);
                }
            }
        }
    }
}
