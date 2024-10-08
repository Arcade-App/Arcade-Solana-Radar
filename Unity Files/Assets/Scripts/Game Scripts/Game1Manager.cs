using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    public bool canSpawn = true;

    public GameObject[] blockPrefabArray;       // Prefab of the block to spawn
    public float moveSpeed = 5f;         // Speed at which the block moves
    public float minMoveSpeed = 2.5f;   // Minimum delay before the next block spawns
    public float maxMoveSpeed = 7.5f;     // Maximum delay before the next block spawns
    public float spawnHeightOffset = 0.65f;  // Height difference between spawned blocks
    public float minSpawnDelay = 0.1f;   // Minimum delay before the next block spawns
    public float maxSpawnDelay = 1f;     // Maximum delay before the next block spawns

    private float screenHalfWidth;       // Half of the screen width in world units
    public float currentSpawnHeight = -2.85f;  // Current Y position for spawning blocks
    private GameObject currentBlock;     // Reference to the currently moving block
    public Vector3 targetPosition;      // The target position for the current block



    //public SpriteRenderer playerSprite;

    public Vector3 blockIinitialStartPosition;

    private void Start()
    {

        blockIinitialStartPosition = new Vector3(0, currentSpawnHeight, 0);
        // Calculate the half width of the screen in world units
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        canSpawn = false;

        // Start the block spawning process by spawning the first block

        
        //Invoke(nameof(SpawnInitialBlock), 2f);
        //SpawnNextBlock();
    }

    private void FixedUpdate()
    {
        // If there's a block currently moving, move it towards the target position
        if (currentBlock != null && !Manager.instance.gameManager.isGame1Over)
        {
            currentBlock.transform.position = Vector3.MoveTowards(currentBlock.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the block has reached the target position
            if (currentBlock.transform.position == targetPosition && canSpawn)
            {
                // Block has reached the center, spawn the next block after a random delay
                float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
                Invoke(nameof(SpawnNextBlock), randomDelay);
                currentBlock = null; // Clear the current block reference
            }
        }
    }

    public void SpawnNextBlock()
    {
        if (!Manager.instance.gameManager.isGame1Over)
        {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);


            targetPosition = new Vector3(0f, currentSpawnHeight, 0f);

            // Determine the spawn position (either from the left or right side of the screen)
            float spawnX = Random.Range(0, 2) == 0 ? -screenHalfWidth - 2f : screenHalfWidth + 2f;
            Vector3 spawnPosition = new Vector3(spawnX, currentSpawnHeight, 0f);

            // Spawn the block
            int i = Random.Range(0, blockPrefabArray.Length);
            currentBlock = Instantiate(blockPrefabArray[i], spawnPosition, Quaternion.identity);



            // Set the target position to the center of the screen
            targetPosition = new Vector3(0f, currentSpawnHeight, 0f);

            // Update the spawn height for the next block
            currentSpawnHeight += spawnHeightOffset;

            //if (currentBlock.transform.position.x > 0)
            //{
            //    playerSprite.flipX = true;
            //}
            //else
            //{
            //    playerSprite.flipX = false;
            //}
        }


    }

    public void SpawnInitialBlock()
    {
        moveSpeed = 2;


        targetPosition = new Vector3(0f, currentSpawnHeight, 0f);

        // Determine the spawn position (either from the left or right side of the screen)
        float spawnX = Random.Range(0, 2) == 0 ? -screenHalfWidth - 2f : screenHalfWidth + 2f;
        Vector3 spawnPosition = new Vector3(spawnX, currentSpawnHeight, 0f);

        // Spawn the block
        int i = Random.Range(0, blockPrefabArray.Length);
        currentBlock = Instantiate(blockPrefabArray[i], spawnPosition, Quaternion.identity);



        // Set the target position to the center of the screen
        targetPosition = new Vector3(0f, currentSpawnHeight, 0f);

        // Update the spawn height for the next block
        currentSpawnHeight += spawnHeightOffset;

        //if (currentBlock.transform.position.x > 0)
        //{
        //    playerSprite.flipX = true;
        //}
        //else
        //{
        //    playerSprite.flipX = false;
        //}
    }

    public void DestroyAllBrickGameObjects()
    {
        // Find all GameObjects with the tag "Brick"
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        // Iterate over each GameObject and destroy it
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }

    }

}
