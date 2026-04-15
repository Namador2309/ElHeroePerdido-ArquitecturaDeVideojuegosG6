using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrulla")]
    public float speed = 2f;
    public float distanciaPatrulla = 3f;

    [Header("Daño")]
    public float damage = 10f;
    public float cooldownDaño = 1f;

    private Vector2 puntoInicio;
    private bool moviendoDerecha = true;
    private float timerDaño = 0f;

    void Start()
    {
        puntoInicio = transform.position;
    }

    void Update()
    {
        Patrullar();
        timerDaño -= Time.deltaTime;
    }

    void Patrullar()
    {
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= puntoInicio.x + distanciaPatrulla)
            {
                moviendoDerecha = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= puntoInicio.x - distanciaPatrulla)
            {
                moviendoDerecha = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timerDaño <= 0f)
        {
            HealthSystem.Instance.TakeDamage(damage);
            timerDaño = cooldownDaño;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timerDaño <= 0f)
        {
            HealthSystem.Instance.TakeDamage(damage);
            timerDaño = cooldownDaño;
        }
    }
}