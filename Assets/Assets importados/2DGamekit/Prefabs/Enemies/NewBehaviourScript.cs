using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            HealthSystem health = col.gameObject.GetComponent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(damage); // 👈 ESTE es el correcto
                Debug.Log("Le hizo daño al jugador");
            }
        }
    }
}