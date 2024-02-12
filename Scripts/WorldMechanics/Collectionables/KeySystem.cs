using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    public int keyValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {

            PlayerData playerData = collision.GetComponent<PlayerData>();

            playerData.playerKeys += keyValue;

            Destroy(gameObject);
        }
    }
}
