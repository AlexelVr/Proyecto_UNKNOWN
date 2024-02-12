using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            PlayerData playerData = collision.GetComponent<PlayerData>();

            playerData.playerCoins += coinValue;

            Destroy(gameObject);
        }
    }
}
