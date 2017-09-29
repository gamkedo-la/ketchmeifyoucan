using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckForPlayer : MonoBehaviour
{
    public Text m_HUDText;
    public GameObject m_Guard; //Object from where the Linecast should originate
    public GameObject m_Player;

    private void Awake() {
        //m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CheckSight()
    {
        RaycastHit rayHit;
        if (Physics.Linecast(m_Guard.transform.position, m_Player.transform.position, out rayHit))
        {
            Debug.Log(rayHit.collider.transform.name);
            if (rayHit.collider.gameObject == m_Player)
            {
                //Debug.Log("Caught");
                GameManager.RestartGame("YOU'VE BEEN CAUGHT");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        CheckSight();
    }
}
