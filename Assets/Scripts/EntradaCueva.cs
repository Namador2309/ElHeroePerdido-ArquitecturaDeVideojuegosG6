using UnityEngine;

public class EntradaCueva : MonoBehaviour
{
    public Transform destino;
    private bool jugadorAdentro = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entró algo al trigger: " + other.name);
        if (other.CompareTag("Player"))
        {
            jugadorAdentro = true;
            Debug.Log("Jugador dentro del trigger");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorAdentro = false;
            Debug.Log("Jugador salió del trigger");
        }
    }

    void Update()
    {
        if (jugadorAdentro && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Debug.Log("Teletransportando a: " + destino.position);
            GameObject jugador = GameObject.FindWithTag("Player");
            jugador.transform.position = destino.position;
        }
    }
}