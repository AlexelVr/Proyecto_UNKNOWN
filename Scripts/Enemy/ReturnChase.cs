using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnChase : MonoBehaviour
{
    public EnemyStateController enemyStateController;
    public void ReturnToChase()
    {
        enemyStateController.returnToChaseState = true;
    }
}
