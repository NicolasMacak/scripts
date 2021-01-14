using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    private bool playerInSight;
    private float fieldOfView = 110f;
    private NavMeshAgent agent;
    private SphereCollider col;
    private Vector3 raycasterPoint;

    private WanderingManagement wanderingAgent;
    private Vector3 currentDestination;
    private bool wasPlayerSeen;
 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInSight = false;
        col = GetComponent<SphereCollider>();
        raycasterPoint = GameObject.Find("RayCaster").transform.position;

        agent = GetComponent<NavMeshAgent>();
        wanderingAgent = new WanderingManagement();
        wasPlayerSeen = false;

        setWanderingDestination();

        

        //wanderingPoints = initializeWanderingPoints();
        //print(wanderingPoints.Remove(wanderingPoints[0]));
        //print(wanderingPoints + "size " + wanderingPoints.Count);
        // agent.SetDestination(GameObject.Find("semPod").transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waitSeconds(2f));
        if (playerInSight) { ChasePlayer(); }
        else if (wasPlayerSeen) { LookForPlayer(); }

        if (!playerInSight) { Wander(); }

        //Wander() vybavené na začiatku
        //ChasePlayer()
        //LookForPlayer()
        //Wander

        //if (playerInSight) selectingAction();
        //else Wander();
        //selectingAction();

        // print("Distance " + Vector3.Distance(transform.position, player.transform.position));
    }

    private void ChasePlayer()
    {
        currentDestination = player.transform.position;
        agent.SetDestination(currentDestination);
        
        //setDestination(player.transform.position);
        transform.LookAt(player.transform.position - player.transform.up);
    }

    //private bool hasReachedSpotWherePlayerWasLastSeen()
    //{
    //    print("destination " + hasReachedDestination());
    //    //print("wasPlayerSeen " + wasPlayerSeen);
    //    if (!hasReachedDestination() && wasPlayerSeen)
    //    {
    //        return false;
    //    }
    //    wasPlayerSeen = false;
    //    return true;
    //}

    private void LookForPlayer()
    {
        if (!hasReachedDestination()) { return; }
        
        wasPlayerSeen = false;


        

        //StartCoroutine(waitSeconds(2f));

        
    }

    private IEnumerator waitSeconds(float seconds)
    {
        //print("looking for player");
        yield return new WaitForSeconds(seconds);
        print("waiting ended");
    }

    private void Wander()
    {
        if (hasReachedDestination())
        {
            setWanderingDestination();
        }
    }

    private void setWanderingDestination()
    {
        currentDestination = wanderingAgent.getWanderingPoint(currentDestination);

        agent.SetDestination(currentDestination);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player) EvaluateColisionWithPlayer(other); // if not player, return       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) { PlayerLost(); } // if not player, return
    }

    private void EvaluateColisionWithPlayer(Collider player)
    {
        raycasterPoint = returnRaycasterPoint();
        playerInSight = isPlayerNearby();
        lookForPlayerInRange();
    }

    private void lookForPlayerInRange()
    {
        Vector3 playerDirection = (player.transform.position - player.transform.up) - raycasterPoint;

        //Debug.DrawLine(raycasterPoint, (raycasterPoint + direction.normalized));
        if (isPlayerInFieldOfView(playerDirection) && isPlayerBeingSeen(playerDirection))
        {
            playerInSight = true;
            wasPlayerSeen = true;
        }
    }

    private bool isPlayerInFieldOfView(Vector3 playerDirection)
    {
        float angleBetweenPlayerAndForwardVector = Vector3.Angle(playerDirection, transform.forward);

        // If angle between player and forward vector is less than fieldOfView * 0.5. Player is in the field of view
        return angleBetweenPlayerAndForwardVector < fieldOfView * 0.5f;
        
    }

    private bool isPlayerBeingSeen(Vector3 playerDirection)
    {
        //print("in sight");
        Debug.DrawLine(raycasterPoint, /*transform.forward*/ /*(raycasterPoint + 2*playerDirection.normalized)*/ player.transform.position + player.transform.up, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(raycasterPoint, /*transform.forward*/ playerDirection.normalized, out hit, 2*col.radius))
        {
            //Debug.DrawLine(raycasterPoint, /*transform.forward*/ playerDirection.normalized, Color.red);
            if (hit.collider.gameObject == player)
            {
                //print("isee");             
                return true;
            }
        }

        return false;
    }

    private bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, currentDestination) < 2f;
    }

    private bool isPlayerNearby()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 4f;
    }

    private void PlayerLost()
    {
        playerInSight = false;
    }

    private Vector3 returnRaycasterPoint()
    {
        return transform.position + 3 * transform.up + transform.forward;
    }

}
