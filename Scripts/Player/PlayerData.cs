using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float playerHealth;
    private float actualPlayerHealth;

    public int playerKeys;
    public int playerCoins;

    private Vector2 checkpointPosition;

    private void Start()
    {
        actualPlayerHealth = playerHealth;
    }

    public void UpdatePlayerHealth(float value)
    {
        actualPlayerHealth += value;

        if(actualPlayerHealth <= 0)
        {
            
            RespawnPlayer();
        }
    }

    public void UpdateCheckPoint(Vector2 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
    }
    private void RespawnPlayer()
    {
        transform.position = checkpointPosition;
        actualPlayerHealth = playerHealth;
    }
}
