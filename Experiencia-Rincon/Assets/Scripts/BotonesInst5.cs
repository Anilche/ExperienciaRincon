using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesInst5 : MonoBehaviour
{
    [Header("Lienzos Cuadro 3")]
    [SerializeField] public GameObject lienzoBaseCuadro;
    [SerializeField] public GameObject lienzo1;
    [SerializeField] public GameObject botonRojoLienzo1;
    [SerializeField] public GameObject lienzo2;
    [SerializeField] public GameObject botonRojoLienzo2;
    [SerializeField] public GameObject lienzo3;
    [SerializeField] public GameObject botonRojoLienzo3;

    private int contadorSelecciones = 0;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void FlechaIzq()
    {
        audioManager.PlaySFX(audioManager.seleccionSFX);

        lienzoBaseCuadro.SetActive(false);

        contadorSelecciones = contadorSelecciones - 1;

        if (contadorSelecciones <= 0)
        {
            contadorSelecciones = 3;
        }

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
        lienzo1.SetActive(false);
        lienzo2.SetActive(false);
        lienzo3.SetActive(false);

        if (contadorSelecciones == 1 || contadorSelecciones >= 4)
        {
            lienzo1.SetActive(true);
            contadorSelecciones = 1;
        }
        else if (contadorSelecciones == 2)
        {
            lienzo2.SetActive(true);
        }
        else if (contadorSelecciones == 3)
        {
            lienzo3.SetActive(true);
        }

        hacerAparecerBotonRojo();
    }

    private void hacerAparecerBotonRojo()
    {
        botonRojoLienzo1.SetActive(false);
        botonRojoLienzo2.SetActive(false);
        botonRojoLienzo3.SetActive(false);

        

        switch (Instancia5.GetInstance().eleccionActiva)
        {
            case "Lienzo1":
                botonRojoLienzo1.SetActive(true);
                break;

            case "Lienzo2":
                botonRojoLienzo2.SetActive(true);
                break;

            case "Lienzo3":
                botonRojoLienzo3.SetActive(true);
                break;
        }
    }

    public void Confirmar()
    {
        // Se confirma la selección y se cierra el UI de elecciones
        audioManager.PlaySFX(audioManager.seleccionSFX);
        Instancia5.GetInstance().ConfirmarSeleccion();
    }
}