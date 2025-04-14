using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    public GameObject inspectedObject; 
    public float distance = 2.0f;      
    public float sensitivity = 2.0f;   

    private bool isInspecting = false; 
    private Vector3 originalPosition;  
    private Quaternion originalRotation; 

    private FirstPersonController firstPersonController;

    void Start()
    {
        
        firstPersonController = gameObject.GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInspecting)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f)) // 10f is the max distance of the ray
                {
                    if (hit.collider.gameObject.CompareTag("Inspectable"))
                    {
                        inspectedObject = hit.collider.gameObject;

                        
                        originalPosition = inspectedObject.transform.position;
                        originalRotation = inspectedObject.transform.rotation;

                        
                        inspectedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;

                        isInspecting = true; 

                        
                        if (firstPersonController != null)
                        {
                            firstPersonController.enabled = false;
                        }
                    }
                }
            }
            else
            {
                
                inspectedObject.transform.position = originalPosition;
                inspectedObject.transform.rotation = originalRotation;

                isInspecting = false;
                inspectedObject = null;

                
                if (firstPersonController != null)
                {
                    firstPersonController.enabled = true;
                }
            }
        }

        if (inspectedObject != null && isInspecting)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        
        inspectedObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        inspectedObject.transform.Rotate(Vector3.right, mouseY, Space.World);
    }
}
