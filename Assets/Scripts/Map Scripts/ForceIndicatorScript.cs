using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceIndicatorScript : MonoBehaviour
{
    public Vector3 forceDirection = new Vector3();
    public float gizmoSize = 30f;

    public Vector3 GetForceDirection()
    {
        return forceDirection.normalized;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + forceDirection.normalized * gizmoSize);
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(transform.position, transform.position + forceDirection.normalized * -gizmoSize / 2);
    }
}
