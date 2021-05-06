using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Destination : MonoBehaviour
{
    [SerializeField] EnemyBehaviour eb;
    public GameObject pathFrog;
    public GameObject pathFrogFX;
    public GameObject patrolFrog;
    private Transform[] waypoints;
    private bool onWayBack;
    private Transform closestWaypoint;
    private bool home;
    bool complete;

    // Start is called before the first frame update
    void Start()
    {
        pathFrog.SetActive(false);
        waypoints = eb.waypoints;
        onWayBack = false;
        complete = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //switch active frogs
            //activate path frog
            pathFrog.SetActive(true);
            //set transform to patrol frog
            waypoints = eb.waypoints;
            pathFrog.transform.position = patrolFrog.transform.position;
            pathFrogFX.transform.rotation = new Quaternion();
            pathFrogFX.transform.rotation *= Quaternion.Euler(0, 0, 90);
            
            patrolFrog.SetActive(false);
            var tempMousePosition = Input.mousePosition;
            tempMousePosition.z = -1;
            this.transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
            complete = false;
        }
        //let path frog rach destination
        if(pathFrog.activeInHierarchy)
        {
            float xDist = pathFrog.transform.position.x - this.transform.position.x;
            //get to destination
            float yDist = pathFrog.transform.position.y - this.transform.position.y;
            if (System.Math.Abs(xDist) < 1 && System.Math.Abs(yDist) < 1 && !onWayBack && !complete)
            {
                //pathfrog path finds back to last patrol point
                closestWaypoint = GetClosestWaypoint(waypoints);
                this.transform.position = closestWaypoint.position;
                onWayBack = true;
            }
            else if (Vector3.Distance(closestWaypoint.position, this.transform.position) < 1)
            {
                complete = true;
                onWayBack = false;
                patrolFrog.transform.position = pathFrog.transform.position;
                patrolFrog.transform.rotation = pathFrog.transform.rotation;
                patrolFrog.transform.rotation *= Quaternion.Euler(0, 0, 90);
                for (int i = 0; i < waypoints.Length; i++)
                {
                    if (closestWaypoint.transform == waypoints[i].transform) eb.WaypointIndex = i;
                }

                //change active frogs
                pathFrog.SetActive(false);
                patrolFrog.SetActive(true);

                //home reached

            }
        }

        //change frog rotation because math is hard

    }
    Transform GetClosestWaypoint(Transform[] waypoints)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        for (int i = 0; i< waypoints.Length;i++)
        {
            float dist = Vector3.Distance(waypoints[i].position, currentPos);
            if (dist < minDist)
            {
                tMin = waypoints[i];
                minDist = dist;
            }
        }
        return tMin;
    }
}
