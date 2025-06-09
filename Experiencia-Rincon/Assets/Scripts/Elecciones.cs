using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elecciones : MonoBehaviour
{
    [Header("Boton Interaccion")]
    [SerializeField] private GameObject botonInteraccion;

    [Header("UI Elecciones")]
    [SerializeField] private GameObject uiElecciones; // Referencia al UI de elecciones

    // Botones de elección de los objetos
    [SerializeField] private GameObject botonEleccion1;
    [SerializeField] private GameObject botonEleccion2;
    [SerializeField] private GameObject botonEleccion3;

    [Header("Opciones de Objetos")]
    // Los objetos que se pueden elegir
    [SerializeField] private GameObject objeto1;
    [SerializeField] private GameObject objeto2;
    [SerializeField] private GameObject objeto3;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] private int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private bool jugadorEnRango;
    private static Elecciones instance;
    public bool eleccionActiva { get; private set; } // Indica si la elección está activa o no




    private int faseActual; // Fase actual del jugador, se puede setear desde otro script




    private void Awake()
    {
        uiElecciones.SetActive(false); // Desactiva el UI de elecciones al inicio
        jugadorEnRango = false; // Inicializa el estado del jugador fuera de rango

        instance = this;

        //////////Borrar para setear deesde otro script después ///////////////////
        faseActual = 0; // Inicializa la fase actual del jugador (esto debería ser seteado desde otro script)
    }

    public static Elecciones GetInstance()
    {
        return instance;
    }

    void Update()
    {
        // Si el jugador está en rango y no está hablando, se mostrará el botón de interacción
        if (jugadorEnRango && !DialogoManager.GetInstance().dialogoActivo && faseActual >= numFaseNecesaria)
        {

            //botonInteraccion.SetActive(true);

            // Si el jugador presiona la tecla E, se activa la elección
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiElecciones.SetActive(true);
                Debug.Log("Comienzo de elecciones");
            }
        }
        // Si el jugador no está en rango, se desactiva el botón de interacción
        else
        {
            //botonInteraccion.SetActive(false);
        }
    }

    // Al entrar al trigger se setea que el jugador está en rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
            botonInteraccion.SetActive(false);
        }
    }
}
