using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    //how many degreescan the cone cover
    public float fov = 90f;
    //amount of rays in the cone
    public int rayCount;
    //how far out it can see
    public float viewDist = 50f;
    //angle and point at which we start
    private float startingAngle;
    private Vector3 origin;
    //origin tranform to attach it to
    public Transform body;

    private Mesh mesh;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;

    }
    void Update()
    {

    
        //current angle we are at in the cycle
        float currentAngle = startingAngle;
        //how much to increase the angle per each cycle in the loop
        float angleIncrease = fov / rayCount;
        

        //
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i =0;i<=rayCount;i++)
        {
            Vector3 vertex; 
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), viewDist);
            if(raycastHit2D.collider == null)
            {
                //no hit
                vertex = origin + GetVectorFromAngle(currentAngle) * viewDist;
            }
            else
            {
                //hit object
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;

            currentAngle -= angleIncrease;
        }
        mesh.vertices = vertices;
        mesh.uv =uv;
        mesh.triangles = triangles;   
    }
    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRadians = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
    }
    //returns an angle from a vector 3 in the form of a float
    float GetAngleFromVector(Vector3 direction)
    {
        //normalize the vector becausethatmakes  it easy
        direction = direction.normalized;
        //calcualte the angle in radians using the tengent, convert to degrees
        //really with intellesense knew flaot meant float
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //make sure the angle is positive
        if (angle < 0)
            angle += 360;
        return angle;
    }
    public void setOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void setAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVector(aimDirection) + (fov/2);
    }
}
