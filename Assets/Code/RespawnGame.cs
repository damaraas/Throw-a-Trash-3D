using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGame : MonoBehaviour
{
    public float threshold;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < threshold)
        {
            transform.position = new Vector3(-0.2482409f, 1.887738f, 0f);
        }
    }
}
