using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPScam : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform Ptransform;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotating around Y-Axis
        Ptransform.Rotate(Vector3.up * mouseX);
       
    }

    public void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
   public void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
