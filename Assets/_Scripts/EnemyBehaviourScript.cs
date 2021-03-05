using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum DroneStates
{
    IDLE,
    LOOK,
}

public class EnemyBehaviourScript : MonoBehaviour
{
    [Header("Line of Sight")]
    public bool HasLOS = false;
    public AudioSource droneSound;

    [Header("Navigation")]
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    public float patrolDistance;
    private Animator animator;

    [Header("Attack")]
    public float attackDistance;
    public PlayerBehaviour playerBehaviour;
    public float damageDelay = 1.0f;
    public bool isAttacking = false;
    public float kickForce = 0.01f;
    public float playerDistance;

    private Vector3 startPosition = new Vector3();
    private Vector3 forward = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        startPosition = transform.position;
        forward = transform.forward;
        StartCoroutine(patrol()); // immediately start patrolling
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasLOS)
        {
            navMeshAgent.SetDestination(player.transform.position);
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
            droneSound.Play();

            // stop routines and follow player
            StopCoroutine(patrol());
            StopCoroutine(lostSight());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // start routine for losing sight of player
            StartCoroutine(lostSight());
        }
    }

    IEnumerator lostSight()
    {
        yield return new WaitForSeconds(1);

        // go to player's last known location
        navMeshAgent.SetDestination(player.transform.position);
        yield return new WaitForSeconds(1);

        HasLOS = false;

        // wait until navmesh reaches destination
        while (navMeshAgent.remainingDistance != 0)
        {
            yield return null;
        }

        // look around for player then wait for 4 seconds instead of immediately leaving the area 
        /*animator.SetInteger("AnimState", (int)DroneStates.LOOK);
        yield return new WaitForSeconds(4);*/
        yield return new WaitForSeconds(4);

        // return to start position
        navMeshAgent.SetDestination(startPosition);
        droneSound.Stop();


        // start patrolling again
        StopCoroutine(lostSight());
        StartCoroutine(patrol());
    }

    IEnumerator patrol()
    {
        // while the player hasn't been sighted
        while (!HasLOS)
        {
            // move forward 30 units
            navMeshAgent.SetDestination(startPosition + (forward * patrolDistance));
            yield return null;

            // wait until navmesh reaches destination
            while (navMeshAgent.remainingDistance != 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(5);

            // move back to start position
            navMeshAgent.SetDestination(startPosition);
            yield return null;

            // wait until navmesh reaches destination
            while (navMeshAgent.remainingDistance != 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(5);
        }
    }
}
