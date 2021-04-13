﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;
    public Transform[] waypoints;
    public int alertState;

    private int WaypointIndex;
    private float dist;
    public Transform player;

    public int rayDist;

    // Start is called before the first frame update
    void Start()
    {
        WaypointIndex = 0;
        alertState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        NormalPatrol();
        //eyes
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.right, rayDist);
        Debug.DrawRay(transform.position, hit.point);
        if (hit.collider.name == "Player")
        {
            Debug.Log("player");
        }
    }
    void MoveTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[WaypointIndex].position, moveSpeed * Time.deltaTime);
    }
    void IncreaseIndex()
    {
        WaypointIndex++;
        if (WaypointIndex >= waypoints.Length)
        {
            WaypointIndex = 0;
        }
    }
    void NormalPatrol()
    {
        float totalDeltaAngle = Vector2.Angle(transform.right, (waypoints[WaypointIndex].position - transform.position).normalized);

        Vector2 a = transform.right;
        Vector2 b = (waypoints[WaypointIndex].position - transform.position).normalized;
        Vector2 d = Vector2.Perpendicular(b);

        float dotAD = Vector2.Dot(a, d);
        float rotDir = dotAD > 0 ? 1 : -1;

        float finalDeltaAngle = Mathf.Min(totalDeltaAngle, Time.deltaTime * rotationSpeed);

        transform.rotation *= Quaternion.AngleAxis(finalDeltaAngle * rotDir, Vector3.back);

        float dot = Vector2.Dot(transform.right, (waypoints[WaypointIndex].position - transform.position).normalized);
        if (dot < Mathf.Epsilon)
        {
            Debug.Log("EXACT");
        }

        if (dot > .99 && dot < 1.01)
        {
            MoveTowards();
            dist = Vector2.Distance(transform.position, waypoints[WaypointIndex].position);
            if (dist == 0)
            {
                IncreaseIndex();
            }
        }
    }

    //void SeePlayer()
    //{
    //    float totalDeltaAngle = Vector2.Angle(transform.right, (player.position - transform.position).normalized);

    //    Vector2 a = transform.right;
    //    Vector2 b = (player.position - transform.position).normalized;
    //    Vector2 d = Vector2.Perpendicular(b);

    //    float dotAD = Vector2.Dot(a, d);
    //    float rotDir = dotAD > 0 ? 1 : -1;

    //    float finalDeltaAngle = Mathf.Min(totalDeltaAngle, Time.deltaTime * 15);

    //    transform.rotation *= Quaternion.AngleAxis(finalDeltaAngle * rotDir, Vector3.back);

    //    float dot = Vector2.Dot(transform.right, (waypoints[WaypointIndex].position - transform.position).normalized);
    //    if (dot > .99 && dot < 1.01)
    //    {
    //        //shoot at player
    //    }
    //    else
    //    {
    //        alertState = 0;
    //    }
    //}
}

