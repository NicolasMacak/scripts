using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using static EnemySoundController;
using static BackgroundMusicController;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public RespawnController respawnManager;
    public BackgroundMusicController backgroundMusicManager;

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
        soundManager = this.GetComponent<EnemySoundController>(); 

        agent = GetComponent<NavMeshAgent>();
        wanderingAgent = new WanderingManagement();



        wasPlayerSeen = false;
        setWanderingDestination();
    }

    void Update()
    {
        if (playerInSight) { ChasePlayer(); }

        else if (wasPlayerSeen) { StartCoroutine(InvestigateLocation()); }

        else if (!playerInSight && !wasPlayerSeen) { Wander(); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player) EvaluateColisionWithPlayer(other); // if not player, return       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) { PlayerLost(); } // if not player, return
    }

    private void ChasePlayer()
    {
        StartRunning();
        backgroundMusicManager.PlayClip(BackGroundMusic.ChaseMusic);
        currentDestination = player.transform.position;
        agent.SetDestination(currentDestination);
        transform.LookAt(player.transform.position - player.transform.up);
    }

    private IEnumerator InvestigateLocation()
    {
        StartCoroutine(backgroundMusicManager.FadeIntoAmbientMusic());
        while (!hasReachedDestination())
        {
            yield return null;
        }

        StartCoroutine(TriggerAndResetAnimation("LookForPlayer"));  

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

    private void EvaluateColisionWithPlayer(Collider player)
    {
        raycasterPoint = returnRaycasterPoint();

        if (isPlayerInKillRange())
        {
            KillHimAndWalkAway();
        } 

        if (isPlayerNearby())
        {
            PlayerIsBeingSeen();
            ChasePlayer();
        }

        lookForPlayerInRange();
    }

    private void lookForPlayerInRange()
    {
        Vector3 playerDirection = (player.transform.position + 0.5f * player.transform.up) - raycasterPoint;

        if (isPlayerInFieldOfView(playerDirection) && isPlayerBeingSeen(playerDirection))
        {
            PlayerIsBeingSeen();
        }
        else
        {
            playerInSight = false;
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
        //Debug.DrawLine(raycasterPoint, (player.transform.position + 0.5f * player.transform.up), Color.red);
        RaycastHit hit;
        if(Physics.Raycast(raycasterPoint, /*transform.forward*/ playerDirection.normalized, out hit, 4*col.radius))
        {
            if (hit.collider.gameObject == player)
            {
                //print("iSeeYou");
                return true;
            }
        }

        return false;
    }

    private void KillHimAndWalkAway()
    {
        wasPlayerSeen = false;
        playerInSight = false;
        respawnManager.KillPlayer();
        StartCoroutine(backgroundMusicManager.FadeIntoAmbientMusic());
        StartWalking();
    }

    private bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, currentDestination) < 3f;
    }

    private bool isPlayerNearby()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 4f;
    }

    private bool isPlayerInKillRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 2f;
    }

    private void PlayerIsBeingSeen() 
    {
        playerInSight = true;
        wasPlayerSeen = true;
    }

    private void PlayerLost()
    {
        playerInSight = false;
        StartCoroutine(backgroundMusicManager.FadeIntoAmbientMusic());
        //wasPlayerSeen = false;
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
        StartCoroutine(TriggerAndResetAnimation("StartWalking"));
        soundManager.PlayClipLoop(EnemySoundName.Wandering);
        agent.speed = 2f;
    }
}
