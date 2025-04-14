using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiFallMovement : MonoBehaviour
{
    public static float globalFallSpeed = 3f; // default speed

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.up * globalFallSpeed * Time.deltaTime;

        if (transform.position.y <= -9)
        {
            Destroy(gameObject);
        }
    }
}
