using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 7;
    [SerializeField] private bool isAI = false;
    [SerializeField] private GameObject ball;

    
    public float AISpeed = 3;
    private float movementY;
    private Rigidbody2D rb;
    private Vector2 playerMovement;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AISpeed = PlayerPrefs.GetFloat("AISpeed");
    }
    
    private void FixedUpdate()
    {
        if (!isAI)
        {
            playerMove();
            rb.velocity = (playerMovement * playerSpeed);
        }
        else
        {
            AIMove();
            rb.velocity = (playerMovement * AISpeed);
        }   
    }

    private void playerMove()
    {
        movementY = Input.GetAxisRaw("Vertical");
        playerMovement = new Vector2(0, movementY);
    }

    private void AIMove()
    {
        if (ball.transform.position.y - 0.5f > rb.transform.position.y)
        {
            playerMovement = new Vector2(0, 1);
        }
        else if (ball.transform.position.y + 0.5f < rb.transform.position.y)
        {
            playerMovement = new Vector2(0, -1);
        }
        else
        {
            playerMovement = new Vector2(0, 0);
        }
    }
}