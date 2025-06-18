using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elecciones : MonoBehaviour
{
    [Header("Boton Interaccion")]
    [SerializeField] private GameObject botonInteraccion;

    [Header("Camaras")]
    //[SerializeField] private GameObject camaraJugador; // Referencia a la c�mara principal
    //[SerializeField] private GameObject camaraEleccion; // Referencia a la c�mara de elecciones

    [SerializeField] private CinemachineVirtualCamera vcamJugador;
    [SerializeField] private CinemachineVirtualCamera vcamEleccion;

    [Header("UI Elecciones")]
    [SerializeField] private GameObject uiElecciones; // Referencia al UI de elecciones

    // Botones de elecci�n de los objetos
    [SerializeField] private GameObject botonEleccion1;
    [SerializeField] private GameObject botonEleccion2;
    [SerializeField] private GameObject botonEleccion3;

    [Header("Opciones de Objetos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject objeto1;
    [SerializeField] public GameObject objeto2;
    [SerializeField] public GameObject objeto3;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private bool jugadorEnRango;
    private static Elecciones instance;
    public bool eleccionActiva { get; private set; } // Indica si la elecci�n est� activa o no

    private void Awake()
    {
        uiElecciones.SetActive(false); // Desactiva el UI de elecciones al inicio
        objeto1.SetActive(false); // Desactiva el objeto 1 al inicio
        objeto2.SetActive(false); // Desactiva el objeto 2 al inicio
        objeto3.SetActive(false); // Desactiva el objeto 3 al inicio

        ActivarCamaraJugador(); // Activa la c�mara del jugador al inicio

        jugadorEnRango = false; // Inicializa el estado del jugador fuera de rango

        instance = this;
    }

    public static Elecciones GetInstance()
    {
        return instance;
    }

    void Update()
    {
        // Si el jugador est� en rango y no est� hablando, se mostrar� el bot�n de interacci�n
        if (jugadorEnRango && !DialogoManager.GetInstance().dialogoActivo && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {

            // Si el jugador presiona la tecla E, se activa la elecci�n
            if (Input.GetKeyDown(KeyCode.E) && eleccionActiva == false)
            {
                Debug.Log("Comienzo de elecciones");
                IniciarEleccion();
            }
        }
    }

    // Al entrar al trigger se setea que el jugador est� en rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            jugadorEnRango = true;
            Debug.Log("Jugador en rango de interacci�n de eleccion");
            botonInteraccion.SetActive(true);
        }
    }

    // Al salir del trigger se setea que el jugador no est� en rango
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador fuera de rango de interacci�n");
            botonInteraccion.SetActive(false); // Desactiva el bot�n de interacci�n si el jugador sale del rango
        }
    }

    private void IniciarEleccion()
    {
        eleccionActiva = true; // Marca que la elecci�n est� activa
        uiElecciones.SetActive(true); // Activa el UI de elecciones
        botonInteraccion.SetActive(false); // Desactiva el bot�n de interacci�n

        Cursor.visible = true; // Hace visible el cursor
        Cursor.lockState = CursorLockMode.None;

        ActivarCamaraEleccion(); // Activa la c�mara de elecciones
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
        ActivarCamaraJugador(); // Activa la c�mara del jugador
        uiElecciones.SetActive(false); // Desactiva el UI de elecciones
        eleccionActiva = false; // Marca que la elecci�n ya no est� activa

        Cursor.visible = false; // Hace invisible el cursor
        Cursor.lockState = CursorLockMode.Confined;

        //Si la fase actual es mayor a la necesaria, no se aumenta de fase, porque se estar�a revisitando una anterior
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            GameManager.GetInstance().SetFaseActual(1);
        }
    }
}