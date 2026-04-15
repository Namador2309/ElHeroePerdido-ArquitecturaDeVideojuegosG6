using UnityEngine;

public class BossAutoAttack : MonoBehaviour
{
    public EnemyTest enemyTest;

    public float speed = 2f;
    public float followRange = 6f;
    public float attackRange = 2f;
    public float attackRate = 2f;

    public int damage = 10;

    float nextAttackTime;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        // SEGUIR AL PLAYER
        if (distance < followRange && distance > attackRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }

        // ATACAR
        if (Time.time > nextAttackTime && distance < attackRange)
        {
            Attack();
            nextAttackTime = Time.time + attackRate;
        }
    }

    void Attack()
    {
        // animación
        if (enemyTest != null)
            enemyTest.Animation_6_Attack();

        Debug.Log("💥 Le pego al player");

        // daño real
        HealthSystem health = player.GetComponent<HealthSystem>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}