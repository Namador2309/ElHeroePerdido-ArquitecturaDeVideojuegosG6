using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [Header("Escena a cargar")]
    public string sceneToLoad = "Escena 2";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica que sea el jugador
        if (collision.CompareTag("Player"))
        {
            Debug.Log("🚪 Entrando al siguiente nivel");

            // Cambiar de escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}