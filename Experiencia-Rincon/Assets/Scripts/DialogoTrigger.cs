using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{

    [Header("Boton Interaccion")]
    [SerializeField] private GameObject botonInteraccion;

    [Header("INK JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool jugadorEnRango;

    // Al iniciar, se desactiva el bot�n de interaccion y se setea que el jugador no est� en rano de interacci�n
    private void Awake()
    {
        jugadorEnRango = false;
        botonInteraccion.SetActive(false);
    }

    private void Update()
    {
        // Si el jugador est� en rango, se mostrar� el bot�n de interacci�n, sino, no se mostrar�
        if (jugadorEnRango)
        {
            botonInteraccion.SetActive(true);

            // Si el jugador presiona la tecla E, se activa el di�logo
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ActivarDialogo();
                Debug.Log("Activar di�logo con el JSON: " + inkJSON.text);
            }
        }
        // Si el jugador no est� en rango, se desactiva el bot�n de interacci�n
        else
        {
            botonInteraccion.SetActive(false);
        }

    }

    // Al entrar en el trigger, se activa el bot�n de interacci�n y se setea que el jugador est� en rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    // Al salir del trigger se setea que el jugador no est� en rango
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }

}
