//using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

public class EnemyAi : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        Idle,
        Attacking,
        Chasing,
        Death
    }

    private const float ROTATION_SPEED = 5f;
    GameObject player;
    //GameObject enemy;
    [SerializeField]
    float visionRange;
    [SerializeField]
    float attackingRange;
    [SerializeField]
    float shootingCooldown;
    [SerializeField]
    float speed;
    [SerializeField]
    float fieldOfView = 360;
    [SerializeField]
    float health = 100;
    [SerializeField]
    EnemyState currentState;
    NavMeshAgent navMesh;

    bool playerInSight;
    SphereCollider col;
    CapsuleCollider capsuleCollider;
    Vector3 lastPlayerSeenPosition = Vector3.zero;

    void Start()
    {
        health = 100;
        player = GameObject.Find("Player");
        //player = player.GetComponentInChildren<CapsuleCollider>().gameObject;
        if (player == null)
        {
            Debug.Log("player not found");
        };
        col = GetComponent<SphereCollider>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        //capsuleCollider.

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            currentState = currentState = EnemyState.Death;
        //print(currentState);
        switch (currentState)
        {
            case EnemyState.Idle:
                //check if player in range
                if (playerInSight)
                {
                    currentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Attacking:
                if (playerInSight)
                {
                    if (IsPlayerInAttackingRange()) ;
                    else
                        currentState = EnemyState.Chasing;
                }

                break;
            case EnemyState.Chasing:
                if (playerInSight)
                {
                    if (IsPlayerInAttackingRange())
                        currentState = EnemyState.Attacking;
                    else
                    {
                        MoveTowardsPlayer();
                    }
                }
                else
                {
                    if (Vector3.Distance(lastPlayerSeenPosition, transform.position) < 5)
                        currentState = EnemyState.Idle;
                    else
                    {
                        MoveTowardsPlayer();
                    }
                }
                break;
            case EnemyState.Death:
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (lastPlayerSeenPosition == Vector3.zero)
            return; 
        Vector3 direction = lastPlayerSeenPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, ROTATION_SPEED  * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, lastPlayerSeenPosition, speed * Time.deltaTime);
        Debug.DrawRay(transform.position, lastPlayerSeenPosition);
    }

    private bool IsPlayerInVisionRange()
    {
        return Vector3.Distance(player.transform.position, gameObject.transform.position) < visionRange;
    }
    private bool IsPlayerInAttackingRange()
    {
        return Vector3.Distance(player.transform.position, gameObject.transform.position) < attackingRange;
    }

  
    //Detect perspective field of view for the AI Character
    bool IsInFOV(float range)
    {
        RaycastHit hit;


        //Debug.DrawRay(transform.position + Vector3.up * 1.5f, transform.forward * range, Color.green);
        //Debug.DrawRay(transform.position + Vector3.up * 1.5f, (transform.forward + player.transform.right).normalized * range, Color.green);
        //Debug.DrawRay(transform.position + Vector3.up * 1.5f, (transform.forward - player.transform.right).normalized * range, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward, out hit, range) ||
            Physics.Raycast(transform.position + Vector3.up * 1.5f, (transform.forward + player.transform.right).normalized * range, out hit, range) ||
            Physics.Raycast(transform.position + Vector3.up * 1.5f, (transform.forward - player.transform.right).normalized * range, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }

        }
        return false;
    }
    private void OnTriggerStay(Collider other)
    {
        //print("trigger");

        if (other.gameObject == player)
        {
            //print("hit player");
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            Debug.DrawRay(transform.position + transform.up, direction.normalized * col.radius, Color.green);

            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfView * 0.5f)
            {

                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    //print("angle");
                    playerInSight = true;

                    if (hit.collider.gameObject == player)
                    {
                        //print("player in range");
                        lastPlayerSeenPosition = player.transform.position;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }
    public void takeDamage()
    {
        health -= 20;
    }
    //private void OnCollisionEnterCapsule(Collision collision)
    //{
    //    print(collision.gameObject.name);
    //    if(collision.gameObject.name=="Bullet(clone)")
    //    {
    //        print("hit");
    //    }
    //}
}