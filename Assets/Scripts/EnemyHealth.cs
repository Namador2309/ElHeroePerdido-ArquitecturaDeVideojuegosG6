using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Salud")]
    public int maxHealth = 50;
    private int currentHealth;

    [Header("Animación de Muerte")]
    public Sprite[] deathSprites;      // 👈 arrastra aquí los sprites en orden
    public float frameRate = 0.1f;     // 👈 velocidad entre cada frame
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log($"💀 {gameObject.name} recibió {damage} daño. Salud: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(PlayDeathAnimation());
    }

    System.Collections.IEnumerator PlayDeathAnimation()
    {
        foreach (Sprite frame in deathSprites)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(frameRate);
        }
        Destroy(gameObject);
    }
}