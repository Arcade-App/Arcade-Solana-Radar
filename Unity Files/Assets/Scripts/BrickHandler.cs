using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHandler : MonoBehaviour
{

    public GameObject topColliderGO;
    public GameObject leftColliderGO;
    public GameObject rightColliderGO;


    public void DestroyAllColliders()
    {
        Destroy(topColliderGO);
        Destroy(leftColliderGO);
        Destroy(rightColliderGO);
    }

}
