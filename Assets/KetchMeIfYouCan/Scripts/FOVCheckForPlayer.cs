using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class FOVCheckForPlayer : MonoBehaviour {

    FieldOfView fov;
    Transform player;
    private void Awake() {

        fov = GetComponent<FieldOfView>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        
        if (Vector3.Distance(transform.position, player.position) < fov.viewRadius) {
            if (CheckPlayer()) {
                GameManager.RestartGame("You're done.");
            }
        }
    }

    //Checks if player is within the field of view.
    bool CheckPlayer() {

        if (fov.visibleTargets.Contains(player))
            return true;

        else return false;
    }
}
