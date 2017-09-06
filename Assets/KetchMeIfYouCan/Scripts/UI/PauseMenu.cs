using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    Transform player;
    Transform menuDisplay;
    private void Awake() {
        menuDisplay = transform.Find("MenuDisplay");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        menuDisplay.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume() {
        Cursor.visible = false;
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        menuDisplay.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel() {
        Cursor.visible = false;
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        menuDisplay.gameObject.SetActive(false);
        Time.timeScale = 1;

        //Replays current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame() {
        //For now just quits the application.
        //Maybe can go to title screen or something.
        Application.Quit();
    }
}
