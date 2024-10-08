using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{

    public float speed = 2f;
    public bool isTriggered = false;
    public GameObject scoreColliderGO;
    public GameObject triggerColliderGO;
    public GameObject bottomColliderGO;

    public GameObject topHoopGO;
    public GameObject bottomHoopGO;

    public Game2Manager game2Manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (game2Manager.canSpawn)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x <= -3f)
        {

            if (!isTriggered)
            {
                Debug.Log("Trigger Out Of Bound GameOver");

                GameObject.Find("Game 2 Player").GetComponent<Game2Player>().canFlap = false;
                Manager.instance.gameManager.ShowGameOver();

            }
            Destroy(gameObject, 1f);
        }

    }
}
