using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public bool isConstantDamage;
    public float damage = 10;

    public float damageCoolDown;
    private float damageTimer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerData playerData = collision.GetComponent<PlayerData>();
            playerData.UpdatePlayerHealth(-damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isConstantDamage)
        {
            return;
        }

        if (collision.tag == "Player")
        {
            PlayerData playerData = collision.GetComponent<PlayerData>();

            damageTimer += Time.deltaTime;

            if(damageTimer > damageCoolDown)
            {
                playerData.UpdatePlayerHealth(-damage);
                damageTimer = 0;
            }
        }
    }
}
