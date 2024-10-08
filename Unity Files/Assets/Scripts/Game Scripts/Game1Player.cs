using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Player : MonoBehaviour
{
    public float jumpForce = 4f;  // Force applied when jumping
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component
    public bool canJump = true;

    public float playerYPos = -2.53f;

    public Game1Manager game1Manager;

    public Game1CameraMovement game1CameraMovement;

    //public UIManager uIManager;

    public AudioClip player1JumpAudioClip;   
    public AudioSource player1AudioSource;


    private void Start()
    {
        // Get the Rigidbody2D component attached to this game object
        rb = GetComponent<Rigidbody2D>();
        canJump = false;
    }

    private void Update()
    {
        // Check if the left mouse button (or screen touch) is pressed
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Apply a vertical force to the player to make them jump
        player1AudioSource.PlayOneShot(player1JumpAudioClip);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
        game1Manager.canSpawn = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Check if the collision is with the player
    //    if (collision.gameObject.CompareTag("Top Collider"))
    //    {
    //        Debug.Log("TOP");
    //        //collision.gameObject.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    //        gameManager.targetPosition = collision.gameObject.GetComponentInParent<Transform>().position;
    //        cameraMovement.OnSuccessfulJump();
    //    }

    //    if (collision.gameObject.CompareTag("Side Collider"))
    //    {
    //        Debug.Log("Side");
    //        gameManager.canSpawn = false;

    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Top Collider"))
        {

            if (!Manager.instance.gameManager.isGame1Over)
            {


                if ((transform.position.y - game1Manager.spawnHeightOffset) < collision.gameObject.transform.position.y)
                {
                    Debug.Log("Side");
                    game1Manager.canSpawn = false;
                    canJump = false;


                    collision.GetComponentInParent<BrickHandler>().DestroyAllColliders();


                    //uIManager.ShowGameOver();

                    Manager.instance.gameManager.ShowGameOver();

                }
                else
                {
                    Debug.Log("TOP");
                    //collision.gameObject.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    game1Manager.targetPosition = collision.gameObject.GetComponentInParent<Transform>().position;
                    canJump = true;
                    game1Manager.canSpawn = true;
                    collision.GetComponentInParent<BrickHandler>().DestroyAllColliders();


                    if (transform.position.y > -0.75)
                    {
                        game1CameraMovement.OnSuccessfulJump();
                    }


                    Debug.Log("Increase score by 10");



                    Manager.instance.gameManager.currentScore += 10;


                    Manager.instance.gameManager.uiOngoingScoreTextArray[0].text = Manager.instance.gameManager.currentScore.ToString();

                }






            }

            
        }
        else if (collision.gameObject.CompareTag("Side Collider"))
        {

            if (!Manager.instance.gameManager.isGame1Over)
            {
                Debug.Log("Side");
                game1Manager.canSpawn = false;
                canJump = false;
                collision.GetComponentInParent<BrickHandler>().DestroyAllColliders();


                //uIManager.ShowGameOver();

                Manager.instance.gameManager.ShowGameOver();

            }



        }
    }
}
