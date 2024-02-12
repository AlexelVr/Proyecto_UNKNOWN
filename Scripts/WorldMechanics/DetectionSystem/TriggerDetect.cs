using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetect : MonoBehaviour
{
    public GameObject door1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerData playerData = collision.GetComponent<PlayerData>();


            if (playerData.playerKeys >= 1)
            {
                door1.SetActive(false);
            }

        }
    }
   
}
