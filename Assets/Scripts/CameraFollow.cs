    using System;
    using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Quaternion cameraRot;
    
    public Transform target;

    public float distance = 5f;
    public float minDistance = 1f;
    public float maxDistance = 7f;

    public Vector3 offset;

    public float smoothSpeed = 5f;
    public float scroolSensivity = 1;

    public float minXRotAngle = -80; //min angle around x axis
    public float maxXRotAngle  = 80; // max angle around x axis

    //Mouse rotation related
    public float rotX; // around x
    public float rotY; // around y
    
    [Range(0.1f, 5f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    public float mouseRotateSpeed = 0.8f;

    [Tooltip("Smaller positive value means smoother rotation, 1 means no smooth apply")]
    public float slerpValue = 0.25f; 
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
            rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
        }

        if (rotX < minXRotAngle)
        {
            rotX = minXRotAngle;
        }
        else if (rotX > maxXRotAngle)
        {
            rotX = maxXRotAngle;
        }
    }

    private void LateUpdate()
    {
        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scroolSensivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 pos = target.position + offset;
        pos -= transform.right * distance;

        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed);

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion newQ;
        newQ  = Quaternion.Euler(rotX , rotY, 0);
        cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue);
        transform.position = target.position + cameraRot * dir;
        transform.LookAt(target.position);
    }

}
