using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum AIState { Wait, Choosing, Walk }

public class GuardAI : MonoBehaviour
{
    public GameObject m_Player;
    public float m_GuardSpeed = 3.0f;
    public float m_GuardRotationSpeed = 3.0f;
    public GameObject[] m_PatrolDestinations;
    public int m_CurrentPatrolDestination = -1;
    public int m_NextPatrolDestination = 0;
    public AIState m_AIState = AIState.Wait;
    public NavMeshAgent m_nav;

    private void Awake()
    {
        m_nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //Set speed of Guard.
        m_nav.speed = m_GuardSpeed;

        //Move Guard to first patrol position in m_PatrolDestinations array.
        gameObject.transform.position = m_PatrolDestinations[0].transform.position;
    }

    private void Update()
    {
        FixedMovement();
    }

    private void FixedMovement()
    {
        if (m_AIState == AIState.Wait)
        {
            StartCoroutine(WaitBeforeContinuingPatrol());
        }

        if (m_AIState == AIState.Choosing)
        {
            StopAllCoroutines();
            FindNextDestination();
        }

        if (m_AIState == AIState.Walk)
        {
            if (m_nav.remainingDistance <= m_nav.stoppingDistance && !m_nav.pathPending)
            {
                m_CurrentPatrolDestination++;
                //If Guard is back at the beginning of his patrol route, reset current destination back to 0.
                if (m_CurrentPatrolDestination > m_PatrolDestinations.Length - 1)
                {
                    m_CurrentPatrolDestination = 0;
                }

                m_NextPatrolDestination++;
                //If Guard has reached the end of his patrol route, return him to beginning.
                if (m_NextPatrolDestination > m_PatrolDestinations.Length - 1)
                {
                    m_NextPatrolDestination = 0;
                }

                m_AIState = AIState.Wait;
            }
        }
    }

    private void FindNextDestination()
    {
        //Set nav destination to next position in array.
        m_nav.SetDestination(m_PatrolDestinations[m_NextPatrolDestination].transform.position);

        m_AIState = AIState.Walk;
    }

    private IEnumerator WaitBeforeContinuingPatrol()
    {
        var patrolDestinationData = m_PatrolDestinations[m_CurrentPatrolDestination].GetComponent<PatrolDestinationData>();
        //Rotate Guard direction of patrol position gameObject.
        transform.rotation = Quaternion.Slerp(transform.rotation, m_PatrolDestinations[m_CurrentPatrolDestination].transform.localRotation, m_GuardRotationSpeed * Time.deltaTime);

        ////Wait at location.
        yield return new WaitForSeconds(patrolDestinationData.m_WaitTime);

        m_AIState = AIState.Choosing;
    }
}