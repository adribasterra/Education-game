﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataAndSaveSystem;

public class Spawner : MonoBehaviour
{

    public enum GizmoType { Never, SelectedOnly, Always }

    public Boid prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public Color colour;
    public GizmoType showSpawnRegion;
    public Transform erPapa;

    void Awake ()
    {
        spawnCount = GameData.boidsCount;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            Boid boid = Instantiate (prefab, erPapa);
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;

            boid.GetComponent<RandomForm>().setRandomForm();
            //boid.SetColour (colour);
        }
    }

    private void OnDrawGizmos ()
    {
        if (showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos ();
        }
    }

    void OnDrawGizmosSelected ()
    {
        if (showSpawnRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos ();
        }
    }

    void DrawGizmos ()
    {
        Gizmos.color = new Color (colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere (transform.position, spawnRadius);
    }

}