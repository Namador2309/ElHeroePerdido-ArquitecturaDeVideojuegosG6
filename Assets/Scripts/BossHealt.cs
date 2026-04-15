using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator anim;

    [Header("Victoria")]
    public GameObject winCanvas; // 👈 arrastras aquí "Ganador"

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (anim != null)
            anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (anim != null)
            anim.SetTrigger("Death");

        Debug.Log("Boss muerto 💀");

        // detener movimiento
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        // mostrar pantalla de victoria
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }

        // pausar juego
        Time.timeScale = 0f;

        // desactivar script
        this.enabled = false;
    }
}