using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public enum States
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public States currentState;

    public int setDirection;
    public int lastDirection;

    [Header("Idle Settings")]
    public float minTime = 0.5f;
    public float maxTime = 3f;
    private float idleCountTime;
    public float idleResetTime;

    [Header("Patrol Settings")]
    public float patrolSpeed;

    [Header("Chase Settings")]
    public float chaseSpeed;
    public Transform senseCenter;
    public float senseRadius;
    private Transform playerTransform;
    public float searchTime;
    private float searchTimeCount;

    [Header("Attack Settings")]
    public float attackDistance;
    public bool returnToChaseState;
    public Animator animator;

    [Header("Collision Settings")]
    public float centerOffset;
    public LayerMask detectLayer;

    [Header("Wall Collision Settings")]
    public float wallCheckDistance;

    [Header("Fall Collision Settings")]
    public float stepDistance; 
    public float fallHeightLimit;

    [Header("Jump Collision Settings")]
    public float jumpRayHeight; 
    public float jumpRayDistance; 



    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    private void Start()
    {
        setDirection = 1;
        lastDirection = setDirection;
        idleResetTime = Random.Range(minTime, maxTime);

        enemyMovement.ChangeSpeed(patrolSpeed);
    }
    void Update()
    {
        enemyMovement.EnemyInputManager(setDirection, JumpDetector());

        EnemyStateMachineBehavior();
        SetAnimatorParameters();
    }

    private void EnemyStateMachineBehavior()
    {
        switch (currentState)
        {
            case States.Idle:
                EnemyIdle();
                break;

            case States.Patrol:
                EnemyPatrol();
                break;

            case States.Chase:
                EnemyChase();
                break;

            case States.Attack:
                EnemyAttack();
                break;

        }
    }

    private void SetAnimatorParameters()
    {
        animator.SetFloat("horizontalSpeed", Mathf.Abs(enemyMovement.xSpeed));
    }

    private void EnemyIdle()
    {
        if (DetectPlayer())
        {
            ChangeState(States.Chase);
            idleCountTime = 0;

            enemyMovement.ChangeSpeed(chaseSpeed);

            return;
        }

        idleCountTime += Time.deltaTime;

        if (idleCountTime < idleResetTime)
        {

        }

        if (idleCountTime > idleResetTime)
        {
            setDirection = lastDirection * -1;
            ChangeState(States.Patrol);
            enemyMovement.ChangeSpeed(patrolSpeed);

            idleCountTime = 0;
        }
    }
    private void EnemyPatrol()
    {
        if (DetectPlayer())
        {
            ChangeState(States.Chase);

            enemyMovement.ChangeSpeed(chaseSpeed);

            return;
        }

        if (WallDetector() || !FallDetector())
        {
            lastDirection = setDirection;
            setDirection = 0;
            idleResetTime = Random.Range(minTime, maxTime);
            ChangeState(States.Idle);
        }
    }
    private void EnemyChase()
    {
        Vector2 enemyPositionX = transform.position.x * Vector2.right;
        Vector2 playerPositionX = playerTransform.position.x * Vector2.right;

        float distance = Vector2.Distance(enemyPositionX, playerPositionX);

        setDirection = (int)Mathf.Sign(playerPositionX.x - enemyPositionX.x);

        if (!DetectPlayer())
        {
            searchTimeCount += Time.deltaTime;

            if (searchTimeCount > searchTime)
            {
                lastDirection = setDirection;
                setDirection = 0;
                idleResetTime = Random.Range(minTime, maxTime);
                ChangeState(States.Patrol);

                searchTimeCount = 0;

                return;
            }
        }

        if (distance < attackDistance)
        {
            setDirection = 0;
            animator.SetTrigger("Attack");
            ChangeState(States.Attack);
        } 
        
        else if (WallDetector() || !FallDetector())
        {
            lastDirection = setDirection;
            setDirection = 0;
            idleResetTime = Random.Range(minTime, maxTime);
            ChangeState(States.Idle);
        }

    }
    private void EnemyAttack()
    {
        if (returnToChaseState)
        {
            ChangeState(States.Chase);
            returnToChaseState = false;
        }
    }

    private void ChangeState(States newState)
    {
        currentState = newState;
    }


    private bool WallDetector()
    {
        Vector3 originPoint = transform.position + Vector3.up * centerOffset;
        bool wallDetect = Physics2D.Raycast(originPoint, Vector3.right * setDirection, wallCheckDistance, detectLayer);

        DebugRay(originPoint, Vector3.right * setDirection, wallCheckDistance, wallDetect);

        return wallDetect;
    }

    private bool FallDetector()
    {
        Vector3 originPoint = transform.position + Vector3.up * centerOffset + Vector3.right * stepDistance * setDirection;

        bool fallDetect = Physics2D.Raycast(originPoint, Vector3.down, fallHeightLimit, detectLayer);
        DebugRay(originPoint, Vector3.down, fallHeightLimit, fallDetect);

        return fallDetect;
    }

    private bool JumpDetector()
    {
        Vector3 centerPoint = transform.position + Vector3.up * centerOffset;
        Vector3 topPoint = transform.position + Vector3.up * jumpRayHeight;

        bool centerRayCast = Physics2D.Raycast(centerPoint, Vector3.right * setDirection, jumpRayDistance, detectLayer);
        bool topRayCast = Physics2D.Raycast(topPoint, Vector3.right * setDirection, jumpRayDistance, detectLayer);

        DebugRay(centerPoint, Vector3.right * setDirection, jumpRayDistance, centerRayCast);
        DebugRay(topPoint, Vector3.right * setDirection, jumpRayDistance, topRayCast);

        if (!topRayCast && centerRayCast)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool DetectPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(senseCenter.position, senseRadius);

        bool detectPlayer = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player")
            {
                playerTransform = colliders[i].transform;
                detectPlayer = true;
            }
        }

        return detectPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(senseCenter.position, senseRadius);
    }

    private void DebugRay(Vector3 origin, Vector3 direction, float distance, bool detectCollision)
    {
        if (detectCollision)
        {
            Debug.DrawRay(origin, direction * distance, Color.green);
        }
        else
        {
            Debug.DrawRay(origin, direction * distance, Color.blue);
        }
    }
}
