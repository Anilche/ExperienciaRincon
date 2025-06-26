using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int faseAhora; // Fase actual del jugador, se puede setear desde otro script

    private static GameManager instance;

    private void Update()
    {
        /*if (faseAhora == 1)
        {

        }*///
    }

    void Start()
    {
        faseAhora = 0; // Inicializa la fase actual del jugador
    }

    private void Awake()
    {
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
    public void SetFaseActual(int nuevaFase)
    {
        GameManager.GetInstance().faseAhora = GameManager.GetInstance().faseAhora + nuevaFase; // Actualiza la fase actual del jugador modificando la variable ubicada en GameManager
        //faseActual = faseActual + nuevaFase; // Actualiza la fase actual del jugador modificando la variable ubicada en GameManager
        Debug.Log("Nueva fase actual: " + GameManager.GetInstance().faseAhora);
    }
}
