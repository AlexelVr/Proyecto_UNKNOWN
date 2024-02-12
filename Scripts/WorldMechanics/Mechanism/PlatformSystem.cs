using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSystem : MonoBehaviour
{
    public float platfromSpeed;

    public Transform platform;
    public Transform pointA;
    public Transform pointB;
    private Vector3 target;

    private void Start()
    {
        target = pointB.position;
    }

    private void FixedUpdate()
    {
        PlatformMovement();
    }

    private void PlatformMovement()
    {
        float distance = Vector3.Distance(platform.position, target);

        if(distance < 0.05f && target == pointB.position)
        {
            target = pointA.position;
        }
        else if(distance < 0.05f && target == pointA.position)
        {
            target = pointB.position;
        }

        platform.position = Vector3.MoveTowards(platform.position, target, platfromSpeed * Time.fixedDeltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.SetParent(platform.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
