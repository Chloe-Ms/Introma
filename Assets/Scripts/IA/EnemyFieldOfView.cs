using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField] EnemyMovement _enemyMovement;

    [Space (10)]
    [SerializeField] float radius; //Radius of the field
    public float Radius
    {
        get
        {
            return radius;
        }
    }
    [Range(0, 360)]
    [SerializeField] float angle; //Angle of the field
    public float Angle
    {
        get
        {
            return angle;
        }
    }
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    public bool canSeePlayer;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Transform target = null;
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, radius, targetMask); //Get colliders in range radius

        if (collidersInRange.Length != 0)
        {
            target = collidersInRange[0].transform; //Only one collider Player
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) // if player in angle
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) // if player at a good distance
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;

        if (canSeePlayer)
        {
            _enemyMovement.SetEnemyState(EnemyState.Follow);
            if (target != null)
            {
                _enemyMovement.SetPlayerReference(target);
            }
        } else
        {
            _enemyMovement.SetEnemyState(EnemyState.Walk);
        }
        
    }
}
