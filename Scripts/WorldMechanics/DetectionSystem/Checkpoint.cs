using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerData playerData = collision.GetComponent<PlayerData>();
            playerData.UpdateCheckPoint(transform.position);
        }
    }
}
