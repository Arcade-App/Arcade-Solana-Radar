using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Manager : MonoBehaviour
{

    public GameObject hoopPrefab;
    public float spawnYPosition;
    public float minSpawnYPosition;
    public float maxSpawnYPosition;

    public float spawnXPosition;

    public bool canSpawn = false;

    public float hoopSpawnRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnHoop", 1f, hoopSpawnRate);

        //StartCoroutine(SpawnHoop());

        canSpawn = false;


    }

    // Update is called once per frame
    void Update()
        {
        
        }




    public IEnumerator SpawnHoop()
    {
        while (canSpawn)
        {
            // Generate a random Y position for the hoop
            spawnYPosition = Random.Range(minSpawnYPosition, maxSpawnYPosition);

            // Instantiate the hoop at the random Y position and set X position
            GameObject hoop = Instantiate(hoopPrefab, new Vector3(spawnXPosition, spawnYPosition, 0), Quaternion.identity);
            hoop.GetComponent<HoopController>().game2Manager = this;
            hoop.GetComponent<HoopController>().topHoopGO.GetComponent<SpriteRenderer>().color = Manager.instance.gameManager.gam2ObstacleColorArray[Manager.instance.gameDataManager.gameBackgroundId];
            hoop.GetComponent<HoopController>().bottomHoopGO.GetComponent<SpriteRenderer>().color = Manager.instance.gameManager.gam2ObstacleColorArray[Manager.instance.gameDataManager.gameBackgroundId];


            // Wait for the specified hoop spawn rate
            yield return new WaitForSeconds(hoopSpawnRate);
        }
    }


    public void DestroyAllHoopGameObjects()
    {

        canSpawn = false;
        StopCoroutine(SpawnHoop());


        // Find all GameObjects with the tag "Brick"
        GameObject[] hoops = GameObject.FindGameObjectsWithTag("Hoop");

        // Iterate over each GameObject and destroy it
        foreach (GameObject hoop in hoops)
        {
            Destroy(hoop);
        }

    }

}
