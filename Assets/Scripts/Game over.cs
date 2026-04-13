using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public TMP_Text Puntos;
    public GameObject gameover; // arrastra aquí el panel en Unity

    public void MostrarGameOver()
    {
        gameover.SetActive(true);
    }
    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }


}



