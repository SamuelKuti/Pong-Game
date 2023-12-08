using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{

    [SerializeField] private float speed = 15f;
    [SerializeField] private float speedIncrement = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;
    [SerializeField] private Text endMessage;
    [SerializeField] private GameObject ScoreCanvas;
    [SerializeField] private GameObject endCanvas;


    private int hitCount = 0;
    private Rigidbody2D rb;
    private string difficulty;

    Vector2 start = new Vector2(0, 0);
    private Vector2 ballMovement;

    
    void Start()
    {
            rb = GetComponent<Rigidbody2D>();
            ScoreCanvas.SetActive(true);
            endCanvas.SetActive(false);
            Invoke("onStart", 2f);
            difficulty = PlayerPrefs.GetString("difficulty");
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed + (speedIncrement * hitCount));
    }

    private void onStart()
    {
        ballMovement = new Vector2(-1f, 0);
        rb.velocity = ballMovement * (speed + (speedIncrement * hitCount));
    }

    private void resetBall()
    {
        hitCount = 0;
        stopBall();
        Invoke("onStart", 2f);
    }

    private void bounce(Transform myObject)
    {
        hitCount++;

        Vector2 ballPos = transform.position;
        Vector2 paddlePos = myObject.position;

        float xDir;
        float yDir;

        if (transform.position.x > 0)
        {
            xDir = -1;
        }
        else 
        { 
            xDir = 1;
        }

        yDir = (ballPos.y - paddlePos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDir == 0)
        {
            yDir = 0.25f;
        }

        rb.velocity = new Vector2(xDir, yDir) * (speed + (speedIncrement * hitCount));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "User" || collision.gameObject.name == "AI")
        {
            bounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
            if (playerScore.text.Equals("5"))
            {
                stopBall();
                Invoke("endGame", 1f);
            }
            else
            {
                resetBall();
            }
        }
        else
        {
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
            if (AIScore.text.Equals("5"))
            {
                stopBall();
                Invoke("endGame", 1f);
            }
            else
            {
                resetBall();
            }
        }
    }

    private void endGame()
    {

        ScoreCanvas.SetActive(false);

        if (playerScore.text.Equals("5"))
        {
            endMessage.text = "WIN";

            //Add ability to update allowed level if they win

        }
        else
        {
            endMessage.text = "LOSE";
        }
        
        endCanvas.SetActive(true);

    }

    private void stopBall()
    {
        transform.position = start;
        ballMovement = new Vector2(0, 0);
        rb.velocity = ballMovement;
    }
}
