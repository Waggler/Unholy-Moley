using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MoleScript : MonoBehaviour
{

    [Header("ChasingPlayer")]
    public float lookRadius = 10f;
    public float huntRadius = 60f;
    public bool isHunting = false;
    public bool isPlayerSafe;
    private Transform target;
    private NavMeshAgent agent;

    public bool isStunned;
    public bool hasBeenStunned = false;
    public float stunDuration;

    [Header("PatrollingWaypoints")]
    public LayerMask whatIsGround;
    public Transform[] points;
    private int destPoint = 0;
    public float waitDuration;
    private float startSpeed;

    [Header("PatrollingRandom")]
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    public float walkPointRange;
    public int numPatrolPoints;
    private Transform[] patrolPoints;

    [Header("Audio")]
    public AudioSource moleVoice;
    public AudioClip chaseScream;
    public AudioClip breathing;
    public AudioClip moleShrink;
    bool moleScreamed = false;

    public Animator animator;

    public GameObject killBox;
    public GameObject Player;


    private IEnumerator stunRoutine;
    private IEnumerator waitRoutine;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        patrolPoints = new Transform[numPatrolPoints];
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startSpeed = agent.speed;
        animator.SetBool("IsMoving", true);

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }//end Start


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);// find the distance between the mole and the player at all times
        isStunned = GetComponent<Target>().stun;// get the stun bool from target script
        isPlayerSafe = Player.GetComponent<KillBox>().isSafe;// make sure the player is outside of the safe starting area
        stunRoutine = Stunned();// check to see if the mole has been stunned

        if (isStunned == true)// if stunned start the stun coroutine and make sure the Mole can't be stunned again
        {
            StartCoroutine(stunRoutine);
            hasBeenStunned = true;

        }
        if (distance <= lookRadius && isHunting == false && isPlayerSafe == false)// if the player is withing the search radius of the mole and is not safe, the mole starts hunting
        {
            agent.SetDestination(target.position);
            isHunting = true;

            if (distance <= agent.stoppingDistance)// if the mole is close to player turn to face them
            {
                FaceTarget();
            }
        }
        else if (distance <= huntRadius && isHunting == true && isPlayerSafe == false)// if the player is withing the hunt radius of the mole and is not safe, the mole continues hunting
        {
            agent.SetDestination(target.position);

            if (moleScreamed == false)// play the mole scream once when he first starts hunting the player
            {
                moleVoice.PlayOneShot(chaseScream);
                moleScreamed = true;
            } 

            if (distance <= agent.stoppingDistance)// if the mole is close to player turn to face them
            {
                FaceTarget();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 5f)// if the mole can not hunt the player, he continues his patrol
            {
                waitRoutine = Wait();
                StartCoroutine(waitRoutine);
                GotoNextPoint();
            }

        }
    } // END Update


    //This method tells the Mole to travel to the next Waypoint
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }//end GoToNextPoint


    //This method Makes the Mole Randomly Patrol. Currently it is not used
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }//end if

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }//end if

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 5f)
        {
            walkPointSet = false;
        }//end if
    }//end Patrolling


    //This method creates a random walkpoint within a set distance of the Mole. Currently it is not used
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }//end if
    }//end SearchWalkPoint


    //This method causes to Mole to face the player when in close range
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }//end FaceTarget


    //This coroutine causes 
    IEnumerator Stunned()
    {
        killBox.gameObject.SetActive(false);
        agent.speed = 0f;
        animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunDuration);// Waits for Duration
        killBox.gameObject.SetActive(true);
        animator.SetBool("Stunned", false);
        agent.speed = startSpeed;
        isStunned = false;
    }// END Stunned


    public IEnumerator Dead()
    {
        killBox.gameObject.SetActive(false);
        agent.speed = 0f;
        moleVoice.PlayOneShot(moleShrink);
        animator.SetBool("MoleDead", true);
        yield return new WaitForSeconds(1);// Waits for Duration
        SceneManager.LoadScene("Win Screen");
    }// END Dead

    IEnumerator Wait()
    {
        //animator.SetBool("IsMoving", false);
        yield return new WaitForSeconds(waitDuration);// Waits for Duration
        animator.SetBool("IsMoving", true);
    }// END Wait


    /*Old Waypoint Script
 * private void MoveToPoint()
{
    for (int i = 0; i <= patrolPoints.Length; i++)
    {
        agent.SetDestination(patrolPoints[i].transform.position);
    }

}*///End MoveToPoint

    //This method shows the Mole's detection radius in Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }//end OnDrawGizmosSelected
}