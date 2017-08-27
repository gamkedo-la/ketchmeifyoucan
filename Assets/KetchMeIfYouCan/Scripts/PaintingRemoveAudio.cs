using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingRemoveAudio : MonoBehaviour {
    [SerializeField] AudioSource myAudio;
    public GameObject PlayerGO;
    public AudioClip stealPaintingClip;
    public AudioClip stealCaseClip;

    void Update()
    {
        PlayerInteract playerStealingScript = PlayerGO.GetComponent<PlayerInteract>();

        if (playerStealingScript.m_StealingNow)
        {
            if (playerStealingScript.m_StolenItem.tag == "Stealable" || playerStealingScript.m_StolenItem.tag == "ObjectiveItem")
            {
                myAudio.clip = stealPaintingClip;
            }
            if (playerStealingScript.m_StolenItem.name == "DisplayCase")
            {
                myAudio.clip = stealCaseClip;
            }

            myAudio.Play();
            playerStealingScript.m_StealingNow = false;
        }
    }
}
