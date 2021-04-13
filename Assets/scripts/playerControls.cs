using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControls : MonoBehaviour
{
    Rigidbody2D body;

    float vertical;
    float horizontal;
    public float health;

    public float runSpeed = 20.0f;

    public GameObject exit;
    public GameObject enemy;
    private bool playing;
    float exitDist;
    float enmDist;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playing = true;
        healthText = GetComponent<Text>();
        Debug.Log("health: "+health);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (playing) 
        { 
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
        
        if(health <= 0)
        {
            GameOver();
        }
        exitDist = Vector2.Distance(transform.position, exit.transform.position);
        enmDist = Vector2.Distance(transform.position, enemy.transform.position);
        if (exitDist < 7) Win();
        if (enmDist < 36) GameOver();
    }
    void GameOver()
    {
        //display game over text
        playing = false;
        Debug.Log("YOU LOSE");
        Destroy(gameObject);
        Time.timeScale = 0;
    }
    void Win()
    {
        //display win text
        playing = false;
        Debug.Log("YOU WIN");
        Time.timeScale = 0;
    }
}
