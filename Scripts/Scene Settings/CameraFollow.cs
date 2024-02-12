using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    public Transform pointA;
    public Transform pointB;


    public float smoothSpeed;

    private void FixedUpdate()
    {
        Vector3 targetPosition = player.position + cameraOffset;

        Vector3 followPosition = Vector3.Slerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        
        transform.position = new Vector3(
            Mathf.Clamp(followPosition.x, pointA.position.x, pointB.position.x),
            Mathf.Clamp(followPosition.y, pointA.position.y, pointB.position.y),
            followPosition.z);
    }
}
