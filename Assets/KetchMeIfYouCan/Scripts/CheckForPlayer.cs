using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckForPlayer : MonoBehaviour
{
    GameObject m_Player;

    public Text m_HUDText;
    public GameObject m_Body; //Object from where the Linecast should originate

    private void Awake() {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CheckSight()
    {
        RaycastHit rayHit;
        if (Physics.Linecast(m_Body.transform.position, m_Player.transform.position, out rayHit))
        {
            if (rayHit.collider.gameObject.CompareTag("PlayerBody"))
            {
                //Debug.Log("Caught");
                GameManager.RestartGame("YOU'VE BEEN CAUGHT");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckSight();
    }
}
