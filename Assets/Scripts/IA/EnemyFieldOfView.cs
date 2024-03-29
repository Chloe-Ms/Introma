using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField] EnemyMovement _enemyMovement;

    [Space (10)]
    [SerializeField] float radiusFront; //Radius of the field
    public float RadiusFront
    {
        get
        {
            return radiusFront;
        }
    }
    [Range(0, 360)]
    [SerializeField] float angleFront; //Angle of the field
    public float AngleFront
    {
        get
        {
            return angleFront;
        }
    }
    [SerializeField] float radiusBack; //Radius of the field
    public float RadiusBack
    {
        get
        {
            return radiusBack;
        }
    }
    [Range(0, 360)]
    [SerializeField] float angleBack; //Angle of the field
    public float AngleBack
    {
        get
        {
            return angleBack;
        }
    }
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    [HideInInspector] public bool canSeePlayer;
    Coroutine _routineCheckPlayer;

    private void Start()
    {
        _routineCheckPlayer = StartCoroutine(FOVRoutine());

    }

    void OnDisable()
    {
        StopCoroutine(_routineCheckPlayer);
        _routineCheckPlayer = null;
    }

    void OnEnable()
    {
        _routineCheckPlayer = StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        //WaitForSeconds wait = 

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Transform target = null;
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, radiusBack, targetMask); //Get colliders in range radius

        if (collidersInRange.Length != 0)
        {
            target = collidersInRange[0].transform; //Only one collider Player
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(-transform.forward, directionToTarget) < angleBack / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) { // if player at a good distance
                    canSeePlayer = true;
                    Debug.DrawRay(transform.position, directionToTarget);
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }

        collidersInRange = Physics.OverlapSphere(transform.position, radiusFront, targetMask); //Get colliders in range radius+

        if (collidersInRange.Length != 0)
        {
            target = collidersInRange[0].transform; //Only one collider Player
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleFront / 2) // if player in angle
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    // if player at a good distance
                    Debug.DrawRay(transform.position, directionToTarget);
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false || canSeePlayer;
            }
            else
                canSeePlayer = false || canSeePlayer;
        }else if (canSeePlayer)
            canSeePlayer = false;

        if (canSeePlayer)
        {
            _enemyMovement.SetEnemyState(EnemyState.Follow);
            if (target != null)
            {
                _enemyMovement.SetPlayerReference(target);
            }
        }

    }
}
