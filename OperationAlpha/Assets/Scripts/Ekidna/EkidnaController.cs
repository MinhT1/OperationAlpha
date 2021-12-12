using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EkidnaController : MonoBehaviour
{
    public Animation anim;
    float timer = 0.0f;
    public float health;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attack
    public float timeBetweenAttack;
    bool alreadyAttacked;
    public int attackDamage;

    // State
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        health = 2000;
        //whatIsPlayer = LayerMask.GetMask("Player");
        attackDamage = 15;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        // Check if player is in sight/attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (!playerInSightRange && !playerInAttackRange)
        //{
        //    Patroling();
        //}
        if (!playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            Idle();
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            Walk();
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Arrived
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calc a new random point range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        Walk();
        agent.SetDestination(player.position);
    }

    int randomAttackNum = 0;
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //randomAttackNum = UnityEngine.Random.Range(1, 5);
            //WaitForAnimation();
            //RandomAttack(randomAttackNum);
            AttackDown();
            //GameObject.Find("Player").GetComponent<PlayerDamage>().PlayerTakeDamage(attackDamage);
            StartCoroutine(AttackCooldown());
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
    }

    
    public void RandomAttack(int numbers)
After:
    }


    public void RandomAttack(int numbers)
*/
    }


    public void RandomAttack(int numbers)
    {
        //randomAttackNum = UnityEngine.Random.Range(1, 5);
        if (numbers == 1)
        {
            WaitForAnimation();
            AttackSweep();

        }
        else if (numbers == 2)
        {

            WaitForAnimation();
            AttackDoubleCombo();

        }
        else if (numbers == 3)
        {
            WaitForAnimation();
            Attack360();
        }
        else
        {
            WaitForAnimation();
            AttackDown();

        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2);
    }

    private IEnumerator WaitForAnimation()
    {
        do
        {
            yield return null;
        } while (anim.isPlaying);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            Dead();
            Invoke(nameof(DestroySelf), 1f);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void Idle()
    {
        anim.Play("Standing Idle");
    }

    public void Walk()
    {
        anim.Play("Walk");
    }

    /**
    public void Run()
    {
        anim.Play("Run");
    }
    */
    public void AttackDown()
    {
        anim.Play("Attacking Down");
    }

    public void AttackSweep()
    {
        anim.Play("Standing Sweep");
    }

    public void AttackDoubleCombo()
    {
        anim.Play("Melee Double Combo");
    }

    public void Attack360()
    {
        anim.Play("360 Attack");
    }

    public void Dead()
    {
        anim.Play("Death");
    }
}
