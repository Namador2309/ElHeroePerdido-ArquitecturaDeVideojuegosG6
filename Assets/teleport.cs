using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destino;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Intentando teletransportar");

            if (destino != null)
            {
                transform.position = destino.position;
            }
            else
            {
                Debug.Log("Destino es NULL");
            }
        }
    }
}