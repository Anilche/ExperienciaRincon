using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesInst5 : MonoBehaviour
{
    [Header("Lienzos Cuadro 3")]
    [SerializeField] public GameObject lienzoBaseCuadro;
    [SerializeField] public GameObject lienzo1;
    [SerializeField] public GameObject lienzo2;
    [SerializeField] public GameObject lienzo3;

    // Referencia al script para que sea mas fácil de llamar
    [SerializeField] private Instancia5 inst5;

    //private string eleccionActiva;

    private int contadorSelecciones = 0;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //eleccionActiva = inst5.eleccionActiva;
    }

    public void FlechaIzq()
    {
        audioManager.PlaySFX(audioManager.seleccionSFX);

        lienzoBaseCuadro.SetActive(false);

        contadorSelecciones--;

        cambiarLienzo();
    }

    public void FlechaDer()
    {
        audioManager.PlaySFX(audioManager.seleccionSFX);

        lienzoBaseCuadro.SetActive(false);

        contadorSelecciones++;

        cambiarLienzo();
    }

    private void cambiarLienzo()
    {
        if (contadorSelecciones == 1)
        {
            lienzo1.SetActive(true);
            lienzo2.SetActive(false);
            lienzo3.SetActive(false);
        }
        else if (contadorSelecciones == 2)
        {
            lienzo1.SetActive(false);
            lienzo2.SetActive(true);
            lienzo3.SetActive(false);
        }
        else if (contadorSelecciones == 3)
        {
            lienzo1.SetActive(false);
            lienzo2.SetActive(false);
            lienzo3.SetActive(true);

            contadorSelecciones = 0;
        }
    }

    public void Confirmar()
    {
        // Se confirma la selección y se cierra el UI de elecciones
        audioManager.PlaySFX(audioManager.seleccionSFX);
        inst5.ConfirmarSeleccion();
    }
}
