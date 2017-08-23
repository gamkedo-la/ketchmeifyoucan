using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {

    private void Update() {
        FixedMovement();
    }

    public AIType m_AIType = AIType.FixedMovement;
    public AIState m_AIState = AIState.Idle;

    [SerializeField]
    Transform[] m_CameraViewTargets;
    int m_CurrentViewTarget = 0;

    [SerializeField]
    int m_RotateSpeed = 16;

    private void FixedMovement() {
        if (m_AIType == AIType.FixedMovement) {

            if (m_AIState == AIState.Idle) {
                StartCoroutine(WaitBeforeContinuingPatrol());
            }

            if (m_AIState == AIState.Wait) {

            }

            if (m_AIState == AIState.Walk) {

                Vector3 targetDir = m_CameraViewTargets[m_CurrentViewTarget].position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, m_RotateSpeed * Time.deltaTime, 0);

                transform.rotation = Quaternion.LookRotation(newDir);

                if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetDir)) < 1) {

                    m_AIState = AIState.Idle;
                    m_CurrentViewTarget++;
                }
            }

            if (m_AIState == AIState.PlayerFound) {
                //Something happens.
            }
        }
    }

    private void FindNextFixedDestination() {

        if (m_CurrentViewTarget > m_CameraViewTargets.Length - 1) {
            m_CurrentViewTarget = 0;
        }
    }

    private IEnumerator WaitBeforeContinuingPatrol() {

        m_AIState = AIState.Wait;
        yield return new WaitForSeconds(1.0f);
        FindNextFixedDestination();
        m_AIState = AIState.Walk;
    }
}
