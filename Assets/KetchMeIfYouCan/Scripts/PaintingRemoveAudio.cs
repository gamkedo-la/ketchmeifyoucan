using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingRemoveAudio : MonoBehaviour {
    [SerializeField] AudioSource myAudio;
    public GameObject PlayerGO;
    public AudioClip stealPaintingClip;
    public AudioClip stealCaseClip;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	//void Update () {
 //       PlayerInteract playerStealingScript = PlayerGO.GetComponent<PlayerInteract>();
        
 //       if (playerStealingScript.m_StealingNow)
 //       {
 //           if (playerStealingScript.m_StolenItem.name == "Painting")
 //           {
 //               myAudio.clip = stealPaintingClip;
 //           }
 //           if (playerStealingScript.m_StolenItem.name == "DisplayCase")
 //           {
 //               myAudio.clip = stealCaseClip;
 //           }
            
 //           myAudio.Play();
 //           playerStealingScript.m_StealingNow = false;
 //       }
 //   }
}
