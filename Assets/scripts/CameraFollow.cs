using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public float camDistance = 30.0f;
    public float offset;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / camDistance);
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y+offset, transform.position.z);
    }
}
