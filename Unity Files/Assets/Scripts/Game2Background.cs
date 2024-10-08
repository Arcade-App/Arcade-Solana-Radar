using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Background : MonoBehaviour
{

    float speed = 0.5f;
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
            if (transform.position.x <= -5.1f)
            {
                transform.position = new Vector3(transform.position.x + 10.2f, transform.position.y, transform.position.z);
            }
        }

    }
}
