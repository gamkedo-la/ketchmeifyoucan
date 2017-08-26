using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolDestinationData : MonoBehaviour
{
    public float m_WaitTime = 5.0f;
    public string m_GuardDialogue = "";

	public void OnDrawGizmosSelected() // called only in unity editor when this object is selected
	{
		// see https://docs.unity3d.com/ScriptReference/Gizmos.html
		float fov = 45;
		float max = 5;
		float min = 0.1f;

		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		Gizmos.color = Color.red;
		Gizmos.DrawFrustum(Vector3.zero, fov, max, min, 1);
		Gizmos.matrix = temp;
	}

	public void OnDrawGizmos() // called only in unity editor
	{
		// see https://docs.unity3d.com/ScriptReference/Gizmos.html
		float fov = 45;
		float max = 0.25f;
		float min = 0.1f;

		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		Gizmos.color = Color.red;
		Gizmos.DrawFrustum(Vector3.zero, fov, max, min, 1);
		Gizmos.matrix = temp;
	}

}