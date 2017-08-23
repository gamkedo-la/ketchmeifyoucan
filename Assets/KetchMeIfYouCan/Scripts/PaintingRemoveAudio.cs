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
	void Update () {
        PlayerInteract playerStealingScript = PlayerGO.GetComponent<PlayerInteract>();
        
        if (playerStealingScript.stealingNow)
        {
            if (playerStealingScript.stolenItem.name == "Painting")
            {
                myAudio.clip = stealPaintingClip;
            }
            if (playerStealingScript.stolenItem.name == "DisplayCase")
            {
                myAudio.clip = stealCaseClip;
            }
            
            myAudio.Play();
            playerStealingScript.stealingNow = false;
        }
    }
}
