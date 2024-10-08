using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game2Player : MonoBehaviour
{

    public bool canFlap;
    float flapStrength = 4f;

    //public Text scoreText;

    public Rigidbody2D rb;


    public Game2Manager game2Manager;

    public AudioClip player2JumpAudioClip;
    public AudioSource player2AudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canFlap = false;
        rb.gravityScale = 0;

    }

    void Update()
    {

        // Flap mechanic (when the player taps or clicks)
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Flap();
        }


    }

    void Flap()
    {
        if (canFlap)
        {
            player2AudioSource.PlayOneShot(player2JumpAudioClip);
            rb.velocity = Vector2.up * flapStrength;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.name.Contains("Obstacle"))
        {
            canFlap = false;
            Debug.Log("Top/Bottom Obstacle GameOver");
            Manager.instance.gameManager.ShowGameOver();

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Score Collider")
        {
            Debug.Log("Score +10");

            Manager.instance.gameManager.currentScore += 10;
            Manager.instance.gameManager.uiOngoingScoreTextArray[1].text = Manager.instance.gameManager.currentScore.ToString();

            Destroy(collision.gameObject.GetComponentInParent<HoopController>().bottomColliderGO);
            collision.gameObject.GetComponentInParent<HoopController>().triggerColliderGO.SetActive(true);
        }

        if (collision.gameObject.name == "Bottom Collider")
        {
            canFlap = false;
            Debug.Log("Hoop Bottom GameOver");
            Destroy(collision.gameObject.GetComponentInParent<HoopController>().scoreColliderGO);
            Manager.instance.gameManager.ShowGameOver();

        }

        if (collision.gameObject.name == "Trigger Collider")
        {
            Debug.Log("Enabled Trigger");
            collision.gameObject.GetComponentInParent<HoopController>().isTriggered = true;
        }

    }

}
