using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{
    public Transform m_player;

    public float m_Speed = 0.25f;
    public float m_MaxAngle = 45.0f;

    private FieldOfView fov;

    private Vector3 pointA;
    private Vector3 pointB;

    private void Awake() {
        fov = GetComponent<FieldOfView>();
    }

    void Start()
    {
        pointA = transform.eulerAngles + new Vector3(0f, m_MaxAngle, 0f);
        pointB = transform.eulerAngles + new Vector3(0f, -m_MaxAngle, 0f);
    }

    void Update()
    {
        RotateCamera();

        if (fov.visibleTargets.Contains(m_player)) {
            Debug.Log("Player is with the field of view of a security camera.");
        }
    }

    private void RotateCamera()
    {
        float time = Mathf.PingPong(Time.time * m_Speed, 1);
        transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
    }
}
