using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Sets the route of the Guard to the position this object is at.
public class MakeNoise : MonoBehaviour {
    public GuardAI guardToCall;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && Tooltip.CurrentTooltipTransform == transform) {
            SetGuardTo();
        }
    }

    void SetGuardTo() {
        guardToCall.m_DestinationQueue = transform.position;
    }
}
