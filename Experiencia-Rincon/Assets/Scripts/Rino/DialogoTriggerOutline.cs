using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogoTriggerOutline : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

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
        // Si el jugador está en rango y no hay un diálogo activo ni el Rino está en movimiento
        if (jugadorEnRango && !DialogoManager.GetInstance().dialogoActivo && !MovimientoRino.GetInstance().enMovimiento)
        {

            // Highlight
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
            {
                highlight = raycastHit.transform;
                if (highlight.CompareTag("Seleccionable") && highlight != selection)
                {
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.cyan;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 6.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }

            // Selection
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    selection = raycastHit.transform;
                    selection.gameObject.GetComponent<Outline>().enabled = true;

                    Debug.Log(highlight.gameObject);

                    string objetoSeleccionado = highlight.gameObject.name;

                    switch (objetoSeleccionado)
                    {
                        case "Rino":
                            DialogoManager.GetInstance().EntrarModoDialogo(inkJSON);
                            //selection.gameObject.GetComponent<Outline>().enabled = false;
                            break;

                        default:
                            Debug.Log("Objeto no reconocido");
                            break;
                    }

                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                        selection = null;
                    }
                }
            }
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
    
    private void OnTriggerStay(Collider other)
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
            if (selection != null)
            {
                selection.gameObject.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
