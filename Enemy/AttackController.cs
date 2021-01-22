﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using static EnemySoundController;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    private bool playerInSight;
    private float fieldOfView = 110f;
    private NavMeshAgent agent;
    private SphereCollider col;
    private Vector3 raycasterPoint;
    private Animator animator;
    private EnemySoundController soundManager;

    private WanderingManagement wanderingAgent;
    private Vector3 currentDestination;
    private bool wasPlayerSeen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInSight = false;
        col = GetComponent<SphereCollider>();
        animator = this.GetComponent<Animator>();
        //raycasterPoint = GameObject.Find("RayCaster").transform.position;
        soundManager = this.GetComponent<EnemySoundController>(); 

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

        if (playerInSight) { ChasePlayer(); }

        else if (wasPlayerSeen) { StartCoroutine(InvestigateLocation()); }

        else if (!playerInSight && !wasPlayerSeen) { Wander(); }

        //print(animator.GetCurrentAnimatorStateInfo(0).IsName("LookingAround"));

        //print("was " + wasPlayerSeen);

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
        StartRunning();
        currentDestination = player.transform.position;
        agent.SetDestination(currentDestination);
        transform.LookAt(player.transform.position - player.transform.up);
    }

    private IEnumerator InvestigateLocation()
    {
        while (!hasReachedDestination())
        {
            yield return null;
        }

        /*
         zmeny. Bola by funckia triggerLookAround kde sa triggerne lookAround animacia, pokial uz nebezi.
        potom by som iba do tejto dal ifs ktore by sa returnovali kym by animacia bezala         * */

        StartCoroutine(TriggerAndResetAnimation("LookForPlayer"));  // .GetCurrentAnimatorStateInfo(0).IsName("AnimationName")
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("LookingAround"))
        //{
        //    yield return null;
        //}

        yield return new WaitForSeconds(4); // length of LookingAroundAnimation
        wasPlayerSeen = false;
        StartWalking();
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
        Vector3 playerDirection = (player.transform.position + 0.5f * player.transform.up) - raycasterPoint;

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
        //print("in sight radius");
        Debug.DrawLine(raycasterPoint, (player.transform.position + 0.5f * player.transform.up), Color.red);
        RaycastHit hit;
        if(Physics.Raycast(raycasterPoint, /*transform.forward*/ playerDirection.normalized, out hit, 4*col.radius))
        {
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
        return Vector3.Distance(transform.position, currentDestination) < 3f;
    }

    private bool isPlayerNearby()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 4f;
    }

    private void PlayerLost()
    {
        playerInSight = false;
        wasPlayerSeen = false;
    }

    private Vector3 returnRaycasterPoint()
    {
        return transform.position + 1 * transform.up + transform.forward;
    }

    private IEnumerator TriggerAndResetAnimation(string animationId)
    {
        animator.SetTrigger(animationId);

        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger(animationId);
    }

    private void StartRunning()
    {
        StartCoroutine(TriggerAndResetAnimation("StartRunning"));
        agent.speed = 8f;
        soundManager.PlayClipOnce(EnemySoundName.Running);
    }

    private void StartWalking()
    {
        soundManager.PlayClipLoop(EnemySoundName.Wandering);
        agent.speed = 2f;
    }

}
