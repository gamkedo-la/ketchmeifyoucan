using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {
    private Transform player;
    private Transform door;
    private Vector3 originalPosition;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        door = transform.Find("Door").transform;
        originalPosition = door.localPosition;
    }

    float openSpeed = 10;
    float doorDistance = 4;
    private void Update() {
        if (Vector3.Distance(transform.position, player.transform.position) < doorDistance) {
            door.localPosition = Vector3.MoveTowards(door.localPosition, new Vector3(originalPosition.x - 2, originalPosition.y, originalPosition.z),
               openSpeed * Time.deltaTime);
        }
        else {
            door.localPosition = Vector3.MoveTowards(door.localPosition, originalPosition, openSpeed * Time.deltaTime);
        }
    }
}