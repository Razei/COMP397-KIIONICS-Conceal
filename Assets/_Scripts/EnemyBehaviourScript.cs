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
        StartCoroutine(pacing());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(navMeshAgent.remainingDistance);
        if (HasLOS)
        {
            navMeshAgent.SetDestination(player.transform.position);
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        } 
        else
        {

        }
        /*navMeshAgent.SetDestination(player.position);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
            droneSound.Play();
            StopCoroutine(pacing());
            Debug.Log(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        /*animator.SetInteger("AnimState", (int)DroneStates.LOOK);

        float counter = 0;
        float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            Debug.Log(counter);
            yield return null;
        }

        //Done playing. Do something below!
        Debug.Log("Done Playing");*/

        yield return new WaitForSeconds(4);

        /*animator.SetInteger("AnimState", (int)DroneStates.IDLE);*/
        navMeshAgent.SetDestination(startPosition);
        droneSound.Stop();
        HasLOS = false;
        StopCoroutine(waiter());
        StartCoroutine(pacing());
    }

    IEnumerator pacing()
    {
        while (!HasLOS)
        {
            // wait until navmesh reaches destination
            while (navMeshAgent.remainingDistance != 0)
            {
                yield return null;
            }

            navMeshAgent.SetDestination(startPosition + (forward * 30));
            yield return new WaitForSeconds(5);


            // wait until navmesh reaches destination
            while (navMeshAgent.remainingDistance != 0)
            {
                yield return null;
            }

            navMeshAgent.SetDestination(startPosition);
            yield return new WaitForSeconds(5);
        }
    }
}
