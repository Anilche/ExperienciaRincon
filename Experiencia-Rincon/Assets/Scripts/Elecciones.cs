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

    // Botones de elecci�n de los objetos
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

        ActivarCamaraJugador(); // Activa la c�mara del jugador al inicio

        jugadorEnRango = false; // Inicializa el estado del jugador fuera de rango
    }

    void Update()
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            particulas.SetActive(true);
        }

        // Si el jugador est� en rango y no est� hablando, se mostrar� el bot�n de interacci�n
        if (jugadorEnRango && !DialogoManager.GetInstance().dialogoActivo && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {

            // Si el jugador presiona la tecla E, se activa la elecci�n
            if (Input.GetKeyDown(KeyCode.E) && GameManager.GetInstance().eleccionActiva == false)
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
        GameManager.GetInstance().eleccionActiva = true; // Marca que la elecci�n est� activa
        uiElecciones.SetActive(true); // Activa el UI de elecciones
        botonInteraccion.SetActive(false); // Desactiva el bot�n de interacci�n

        Cursor.visible = true; // Hace visible el cursor
        Cursor.lockState = CursorLockMode.None;

        ActivarCamaraEleccion(); // Activa la c�mara de elecciones
        
        // Elegir la opci�n de elecci�n seg�n la fase necesaria
        switch (numFaseNecesaria)
        {
            case 1:

                //Eleccion1();
                break;
            case 2:
                // Eleccion2(); // Implementar la l�gica para la segunda elecci�n
                break;
            case 3:
                // Eleccion3(); // Implementar la l�gica para la tercera elecci�n
                break;
            default:
                Debug.LogWarning("Selecci�n no v�lida");
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
        
        ActivarCamaraJugador(); // Activa la c�mara del jugador
        uiElecciones.SetActive(false); // Desactiva el UI de elecciones
        GameManager.GetInstance().eleccionActiva = false; // Marca que la elecci�n ya no est� activa

        Cursor.visible = false; // Hace invisible el cursor
        Cursor.lockState = CursorLockMode.Confined;

        //Si la fase actual es mayor a la necesaria, no se aumenta de fase, porque se estar�a revisitando una anterior
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            GameManager.GetInstance().SetFaseActual(1);
        }

        particulas.SetActive(false);
        
    }
}