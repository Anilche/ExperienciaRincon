using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elecciones : MonoBehaviour
{
    [Header("Boton Interaccion")]
    [SerializeField] private GameObject botonInteraccion;

    [Header("Camaras")]
    [SerializeField] private CinemachineVirtualCamera vcamJugador;
    [SerializeField] private CinemachineVirtualCamera vcamEleccion;

    [Header("UI Elecciones")]
    [SerializeField] private GameObject uiElecciones; // Referencia al UI de elecciones

    // Botones de elección de los objetos
    [SerializeField] private GameObject botonEleccion1;
    [SerializeField] private GameObject botonEleccion2;
    [SerializeField] private GameObject botonEleccion3;

    [Header("Opciones de Objetos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject objeto1;
    [SerializeField] public GameObject objeto2;
    [SerializeField] public GameObject objeto3;
    /*
    [Header("Animators de Objetos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject animObjeto1;
    [SerializeField] public GameObject animObjeto2;
    [SerializeField] public GameObject animObjeto3;*/

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private bool jugadorEnRango;

    private void Awake()
    {
        uiElecciones.SetActive(false); // Desactiva el UI de elecciones al inicio
        objeto1.SetActive(false); // Desactiva el objeto 1 al inicio
        objeto2.SetActive(false); // Desactiva el objeto 2 al inicio
        objeto3.SetActive(false); // Desactiva el objeto 3 al inicio
        particulas.SetActive(false); // Desactiva las particulas al inicio

        ActivarCamaraJugador(); // Activa la cámara del jugador al inicio

        jugadorEnRango = false; // Inicializa el estado del jugador fuera de rango
    }

    void Update()
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            particulas.SetActive(true);
        }

        // Si el jugador está en rango y no está hablando, se mostrará el botón de interacción
        if (jugadorEnRango && !DialogoManager.GetInstance().dialogoActivo && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {

            // Si el jugador presiona la tecla E, se activa la elección
            if (Input.GetKeyDown(KeyCode.E) && GameManager.GetInstance().eleccionActiva == false)
            {
                Debug.Log("Comienzo de elecciones");
                IniciarEleccion();
            }
        }
    }

    // Al entrar al trigger se setea que el jugador está en rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            jugadorEnRango = true;
            Debug.Log("Jugador en rango de interacción de eleccion");
            botonInteraccion.SetActive(true);
        }
    }

    // Al salir del trigger se setea que el jugador no está en rango
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador fuera de rango de interacción");
            botonInteraccion.SetActive(false); // Desactiva el botón de interacción si el jugador sale del rango
        }
    }

    private void IniciarEleccion()
    {
        GameManager.GetInstance().eleccionActiva = true; // Marca que la elección está activa
        uiElecciones.SetActive(true); // Activa el UI de elecciones
        botonInteraccion.SetActive(false); // Desactiva el botón de interacción

        Cursor.visible = true; // Hace visible el cursor
        Cursor.lockState = CursorLockMode.None;

        ActivarCamaraEleccion(); // Activa la cámara de elecciones
        
        // Elegir la opción de elección según la fase necesaria
        switch (numFaseNecesaria)
        {
            case 1:

                //Eleccion1();
                break;
            case 2:
                // Eleccion2(); // Implementar la lógica para la segunda elección
                break;
            case 3:
                // Eleccion3(); // Implementar la lógica para la tercera elección
                break;
            default:
                Debug.LogWarning("Selección no válida");
                break;
        }
    }

    private void ActivarCamaraJugador()
    {
        vcamJugador.Priority = 10;
        vcamEleccion.Priority = 0;
    }

    private void ActivarCamaraEleccion()
    {
        vcamJugador.Priority = 0;
        vcamEleccion.Priority = 10;
    }

    public void ConfirmarSeleccion()
    {
        
        ActivarCamaraJugador(); // Activa la cámara del jugador
        uiElecciones.SetActive(false); // Desactiva el UI de elecciones
        GameManager.GetInstance().eleccionActiva = false; // Marca que la elección ya no está activa

        Cursor.visible = false; // Hace invisible el cursor
        Cursor.lockState = CursorLockMode.Confined;

        //Si la fase actual es mayor a la necesaria, no se aumenta de fase, porque se estaría revisitando una anterior
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            GameManager.GetInstance().SetFaseActual(1);
        }

        particulas.SetActive(false);
        
    }
}