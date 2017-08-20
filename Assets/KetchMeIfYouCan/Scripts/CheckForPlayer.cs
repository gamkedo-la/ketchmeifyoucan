using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckForPlayer : MonoBehaviour
{
    public Text m_HUDText;
    public GameObject m_Player;

    public void CheckSight()
    {
        RaycastHit rayHit;
        if (Physics.Linecast(gameObject.transform.position, m_Player.transform.position, out rayHit))
        {
            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                GameManager.RestartGame("YOU'VE BEEN CAUGHT!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckSight();
    }
}
