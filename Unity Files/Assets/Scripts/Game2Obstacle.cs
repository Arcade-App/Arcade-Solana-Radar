using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Obstacle : MonoBehaviour
{
    float speed = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x <= -4.5f)
        {
            transform.position = new Vector3(transform.position.x + 9f, transform.position.y, transform.position.z);
        }
    }
}
