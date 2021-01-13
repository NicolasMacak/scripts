using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    private bool playerInSight;
    private float fieldOfView = 110f;
    private NavMeshAgent agent;
    private SphereCollider col;
    private Vector3 raycasterPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInSight = false;
        col = GetComponent<SphereCollider>();
        raycasterPoint = GameObject.Find("RayCaster").transform.position;

        agent= GetComponent<NavMeshAgent>();

        agent.SetDestination(GameObject.Find("semPod").transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
            EvaluateColisionWithPlayer(other); // if not player, return       
    }

    private void EvaluateColisionWithPlayer(Collider player)
    {
        playerInSight = false;
        //print("player");
        Vector3 direction = player.transform.position - raycasterPoint;
        float angle = Vector3.Angle(direction, transform.forward);
        
        // reversed logic for clean code. Explanation: If angle between player and forward vector is less than fieldOfView * 0.5. Player is in the field of view
        if (angle > fieldOfView * 0.5f) return;
        Debug.DrawLine(raycasterPoint, (raycasterPoint + direction.normalized));
        playerInSight = isPlayerBeingSeen(direction);
    }

    private bool isPlayerBeingSeen(Vector3 direction)
    {
        print("in sight");
        RaycastHit hit;
        if(Physics.Raycast(raycasterPoint, /*transform.forward*/ direction.normalized, out hit, 2*col.radius))
        {
            Debug.DrawLine(raycasterPoint, /*transform.forward*/ direction.normalized, Color.red);
            if (hit.collider.gameObject == player)
            {
                print("isee");
                return true;
            }
        }
        else
        {
            //print("not");
        }

        return false;
    }
}
