using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    // Settings
    public float minSpeed = 5;
    public float maxSpeed = 85;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1;
    public float maxSteerForce = 8;

    public float alignWeight = 2;
    public float cohesionWeight = 1;
    public float seperateWeight = 2.5f;

    public float targetWeight = 2;

    [Header ("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = 0.27f;
    public float avoidCollisionWeight = 20;
    public float collisionAvoidDst = 5;

}