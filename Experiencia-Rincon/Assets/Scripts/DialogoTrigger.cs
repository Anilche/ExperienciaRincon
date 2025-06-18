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

    // Al iniciar, se desactiva el botón de interaccion y se setea que el jugador no está en rano de interacción
    private void Awake()
    {
        jugadorEnRango = false;
        botonInteraccion.SetActive(false);
    }

    private void Update()
    {
        // Si el jugador está en rango, se mostrará el botón de interacción
        if (jugadorEnRango && !DialogoManager.GetInstance().dialogoActivo)
        {
            //botonInteraccion.SetActive(true);

            // Si el jugador presiona la tecla E, se activa el diálogo
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogoManager.GetInstance().EntrarModoDialogo(inkJSON);
            }
        }
        // Si el jugador no está en rango, se desactiva el botón de interacción
        else
        {
            //botonInteraccion.SetActive(false);
        }

    }

    // Al entrar en el trigger, se activa el botón de interacción y se setea que el jugador está en rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            botonInteraccion.SetActive(true);
        }
    }

    // Al salir del trigger se setea que el jugador no está en rango
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            botonInteraccion.SetActive(false);
        }
    }
}