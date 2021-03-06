using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;
    public Transform[] waypoints;
    public int alertState;

    public int WaypointIndex;
    private float dist;

    
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
       
        Patrol();
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
    void Patrol()
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
        //-.01389242
        //0.07321681
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
   
}

