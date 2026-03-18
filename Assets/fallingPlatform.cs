using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float respawnTime = 3f;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        startPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!triggered && col.gameObject.CompareTag("Player"))
        {
            triggered = true;



            Invoke("Fall", fallDelay);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

        // empezar proceso de respawn
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        // resetear posición
        transform.position = startPosition;

        // resetear física
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;


        // permitir que vuelva a activarse
        triggered = false;
    }
}