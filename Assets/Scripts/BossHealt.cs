using UnityEngine;

public class BossHealt : MonoBehaviour, IDamageable
{
    [Header("Salud")]
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    [Header("Animación de Muerte")]
    public Sprite[] deathSprites;
    public float frameRate = 0.1f;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    [Header("Victoria")]
    public GameObject winCanvas;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (anim != null)
            anim.SetTrigger("Hit");
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;

        // Detener movimiento
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        // Desactivar collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Desactivar Animator para que no interfiera con los sprites
        if (anim != null) anim.enabled = false;

        // Reproducir animación de muerte
        if (deathSprites.Length > 0 && spriteRenderer != null)
            StartCoroutine(PlayDeathAnimation());
        else
            ShowWinScreen();
    }

    System.Collections.IEnumerator PlayDeathAnimation()
    {
        foreach (Sprite frame in deathSprites)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(frameRate);
        }
        ShowWinScreen();
    }

    void ShowWinScreen()
    {
        if (winCanvas != null)
            winCanvas.SetActive(true);
        Time.timeScale = 0f;
        this.enabled = false;
    }
}