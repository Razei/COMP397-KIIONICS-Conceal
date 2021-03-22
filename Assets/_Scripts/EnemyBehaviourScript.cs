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
    public AudioSource gunSound;

    [Header("Navigation")]
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    private Animator animator;
    public Transform parentPoint;
    private Transform[] points = new Transform[0];
    private int destPoint = 0;

    [Header("Attack")]
    public float attackDistance;
    public PlayerBehaviour playerBehaviour;
    public HealthBarScreenSpaceController healthBar;
    public float damageDelay = 1.0f;
    public bool isAttacking = false;
    public float kickForce = 0.01f;
    public float playerDistance;

    private Bullets bulletScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        healthBar = FindObjectOfType<HealthBarScreenSpaceController>();
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        navMeshAgent.autoBraking = false;
        bulletScript = GetComponent<Bullets>();

        // get all the child points
        if (parentPoint)
        {
            int children = parentPoint.childCount;
            points = new Transform[children];
            for(int i = 0; i < children; ++i)
            {
                points[i] = parentPoint.GetChild(i);
            }
        }

        StartCoroutine(patrol()); // immediately start patrolling
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasLOS)
        {
            navMeshAgent.SetDestination(player.transform.position);
            playerDistance = Vector3.Distance(transform.position, player.transform.position);

            if (!isAttacking)
                StartCoroutine(InflictDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
            droneSound.Play();
            gunSound.Play();
            bulletScript.shootBullet();

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
        HasLOS = false;
        isAttacking = false;
        yield return new WaitForSeconds(1);

        droneSound.Stop();
        gunSound.Stop();

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
        /*navMeshAgent.SetDestination(startPosition);*/
 

        // start patrolling again
        StopCoroutine(lostSight());
        StartCoroutine(patrol());
    }

    IEnumerator patrol()
    {
        // while the player hasn't been sighted
        while (!HasLOS)
        {
            if (points.Length != 0)
            {
                // Set the agent to go to the currently selected destination.
                navMeshAgent.SetDestination(points[destPoint].position);
                yield return null;


                // wait until navmesh reaches destination
                while (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > 0.5f)
                {
                    yield return null;
                }

                // Choose the next point in the array as the destination,
                // cycling to the start if necessary.
                destPoint = (destPoint + 1) % points.Length;
                yield return null;
            }
            else
            {
                yield break;
            }


            /*// move forward 30 units
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

            yield return new WaitForSeconds(5);*/
        }
    }

    IEnumerator InflictDamage()
    {
        isAttacking = true;
        yield return new WaitForSeconds(damageDelay);

        // if the player is still in line of sight after damage delay
        if (HasLOS)
        {
            healthBar.TakeDamage(10);
            playerBehaviour.hitSound.Play();
            isAttacking = false;
            StopCoroutine(InflictDamage());
        }
    }

    private float calculatePathDistance(NavMeshPath path)
    {
        float distance = .0f;
        for (var i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }
        return distance;
    }
}
