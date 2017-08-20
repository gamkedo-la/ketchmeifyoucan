using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIType { FreeMovement, FixedMovement }
public enum AIState { Idle, Walk, Wait, PlayerFound }

public class EnemyAI : MonoBehaviour
{
    public GameObject m_Player;
    public float m_EnemySpeed = 1.4f;
    public GameObject[] m_PatrolDestinations;
    public int m_CurrentPatrolDestination = 0;
    public int m_NextPatrolDestination = 0;
    public float m_MaxSightDistance = 10.0f;
//  public float m_MaxDestinationDistance = 10.0f;

    public AIType m_AIType = AIType.FixedMovement;
    public AIState m_AIState = AIState.Idle;

    private NavMeshAgent m_nav;

    private void Awake()
    {
        m_nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        m_nav.speed = m_EnemySpeed;
    }

    private void Update()
    {
        FixedMovement();
    }

    private void FixedMovement()
    {
        if (m_AIType == AIType.FixedMovement)
        {
            if (m_AIState == AIState.Idle)
            {
                StartCoroutine(WaitBeforeContinuingPatrol());
            }

            if (m_AIState == AIState.Wait)
            {
                m_CurrentPatrolDestination = m_NextPatrolDestination - 1;
                if (m_CurrentPatrolDestination < 0)
                {
                    m_CurrentPatrolDestination = 0;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, m_PatrolDestinations[m_CurrentPatrolDestination].transform.rotation, 3.0f * Time.deltaTime);
                return;
            }

            if (m_AIState == AIState.Walk)
            {
                if (m_nav.remainingDistance <= m_nav.stoppingDistance && !m_nav.pathPending)
                {
                    m_AIState = AIState.Idle;
                    m_CurrentPatrolDestination++;
                }
            }

            if (m_AIState == AIState.PlayerFound)
            {
                Debug.Log("Player caught!");
                //Animate guard
            }
        }
    }

    private void FindNextFixedDestination()
    {   
        m_nav.SetDestination(m_PatrolDestinations[m_NextPatrolDestination].transform.position);
        m_NextPatrolDestination++;
        if (m_NextPatrolDestination > m_PatrolDestinations.Length - 1)
        {
            m_NextPatrolDestination = 0;
        }
    }

    private IEnumerator WaitBeforeContinuingPatrol()
    {
        m_AIState = AIState.Wait;
        yield return new WaitForSeconds(5.0f);
        FindNextFixedDestination();
        m_AIState = AIState.Walk;
    }
}
