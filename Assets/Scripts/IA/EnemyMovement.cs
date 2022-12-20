using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Walk,
    Follow,
    Attack
}
public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints; //Les points de suivi
    private int currentWaypoint;
    private EnemyState _enemyState = EnemyState.Walk;
    private GameObject _player;
    [SerializeField] private float _normalSpeed = 1.5f;
    [SerializeField] private float _runningSpeed = 3;
    [SerializeField] private AudioSource _audioScreamer;

    void Start()
    {
        navMeshAgent.speed = _normalSpeed;
        if (waypoints.Length > 0){
            navMeshAgent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        if (_enemyState != EnemyState.Attack)
        {
            switch (_enemyState)
            {
                case EnemyState.Walk:
                    navMeshAgent.speed = _normalSpeed;
                    navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
                    //Si l'ennemi a atteint le point, on change de point
                    if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && waypoints.Length > 0)
                    {
                        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
                    }
                    break;
                case EnemyState.Follow:
                    navMeshAgent.speed = _runningSpeed;
                    navMeshAgent.SetDestination(_player.transform.position);
                    break;
            }
        }
    }

    public void SetEnemyState(EnemyState newES)
    {
        if (_enemyState != EnemyState.Attack)
        {
            _enemyState = newES;
        }
    }

    public void SetPlayerReference(Transform playerTransform)
    {
        _player = playerTransform.gameObject;
    }

    public void StartScreamer()
    {
        SetEnemyState(EnemyState.Attack);
        navMeshAgent.SetDestination(transform.position);
        _audioScreamer.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            //Debug.Log("PLAYER");
        }
    }
}
