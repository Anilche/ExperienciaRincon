using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arcade : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool estaEnAreaDeInteraccion = false;

    private string tagSeleccionable = "Seleccionable";

    [Header("Pantalla")]
    [SerializeField] public GameObject botonPantalla1;
    [SerializeField] public GameObject botonPantalla2;
    [SerializeField] public GameObject botonPantalla3;

    [Header("Pantallas juegos")]
    [SerializeField] public GameObject pantalla1;
    [SerializeField] public GameObject pantalla2;
    [SerializeField] public GameObject pantalla3;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }


    void Update()
    {

        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria && estaEnAreaDeInteraccion)
        {
            botonPantalla1.tag = tagSeleccionable;
            botonPantalla2.tag = tagSeleccionable;
            botonPantalla3.tag = tagSeleccionable;

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

                float distancia = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, highlight.position);

                if (highlight.CompareTag("Seleccionable") && distancia <= distanciaMaxima)
                {
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
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

                float distancia = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, highlight.position);

                if (highlight && distancia <= distanciaMaxima)
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
                        case "BotonTetris":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            pantalla1.SetActive(true);
                            pantalla2.SetActive(false);
                            pantalla3.SetActive(false);
                            botonPantalla1.SetActive(false);
                            botonPantalla2.SetActive(false);
                            botonPantalla3.SetActive(false);

                            Debug.Log("Tetris");
                            break;

                        case "BotonPacman":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            pantalla1.SetActive(false);
                            pantalla2.SetActive(true);
                            pantalla3.SetActive(false);
                            botonPantalla1.SetActive(false);
                            botonPantalla2.SetActive(false);
                            botonPantalla3.SetActive(false);

                            Debug.Log("Pacman");
                            break;

                        case "BotonPuzzleBobble":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            pantalla1.SetActive(false);
                            pantalla2.SetActive(false);
                            pantalla3.SetActive(true);
                            botonPantalla1.SetActive(false);
                            botonPantalla2.SetActive(false);
                            botonPantalla3.SetActive(false);

                            Debug.Log("Puzzle");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            estaEnAreaDeInteraccion = true;
            Debug.Log("Jugador en area de interaccion calido");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            estaEnAreaDeInteraccion = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaEnAreaDeInteraccion = false;
            Debug.Log("Jugador salio del area de interaccion calido");
        }
    }
}
