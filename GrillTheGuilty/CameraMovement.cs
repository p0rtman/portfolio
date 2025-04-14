using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float maxDistance = 1.0f; 

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            Vector3 direction = horizontalInput < 0 ? Vector3.left : Vector3.right;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            // Only move if no obstacle is detected within maxDistance
            if (!Physics.Raycast(ray, out hit, maxDistance))
            {
                Vector3 move = new Vector3(horizontalInput, 0, 0);
                transform.Translate(move * speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Obstacle detected: " + hit.collider.gameObject.name);
            }
        }
    }
}
