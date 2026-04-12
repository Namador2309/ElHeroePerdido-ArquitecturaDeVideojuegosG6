using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Referencias")]
    public Transform attackPoint;

    [Header("Configuración")]
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public int damage = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("🗡️ Ataqué");

        if (attackPoint == null)
        {
            Debug.LogError("❌ No asignaste AttackPoint");
            return;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        if (hitEnemies.Length == 0)
        {
            Debug.Log("❌ No golpeaste nada");
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("🎯 Golpeando: " + enemy.name);

            BossHealth boss = enemy.GetComponent<BossHealth>();

            if (boss != null)
            {
                boss.TakeDamage(damage);
                Debug.Log("💥 Daño aplicado: " + damage);
            }
            else
            {
                Debug.LogWarning("⚠️ Este objeto no tiene BossHealth");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}