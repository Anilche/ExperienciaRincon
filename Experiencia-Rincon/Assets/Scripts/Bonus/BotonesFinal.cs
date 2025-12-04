using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesFinal : MonoBehaviour
{
    public void BotonSalir()
    {
        Application.Quit();
    }

    public void BotonReiniciar()
    {
        SceneManager.LoadScene("VideoBoxset");
    }
}