using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoleScript : MonoBehaviour
{

    [Header("ChasingPlayer")]
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    [Header("PatrollingRandom")]
    //PatrollingWaypoints
    public LayerMask whatIsGround;
    public Transform[] points;
    private int destPoint = 0;

    [Header("PatrollingRandom")]
    private Transform[] patrolPoints;
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    public float walkPointRange;
    public int numPatrolPoints;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        patrolPoints = new Transform[numPatrolPoints];
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }//end if
        }//end if
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 5f)
                GotoNextPoint();
        }//end else
    }//end Update

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
