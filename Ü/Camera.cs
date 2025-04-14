using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.25f;
    public float boundY = 0.15f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (lookAt.position.x > transform.position.x)
            {
                //subtract
                delta.x = deltaX - boundX;
            }
            else
            {
                //add
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (lookAt.position.y > transform.position.y)
            {
                //subtract
                delta.y = deltaY - boundY;
            }
            else
            {
                //add
                delta.y = deltaY + boundY;
            }
        }

        //move
        transform.position += new Vector3(delta.x, delta.y, 0);

    }
}
