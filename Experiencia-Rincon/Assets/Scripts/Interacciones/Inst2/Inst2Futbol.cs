using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inst2Futbol : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    //private string tagSeleccionable = "Seleccionable";

    [Header("Techos")]
    [SerializeField] public GameObject techoDia;
    [SerializeField] public GameObject techoTarde;
    [SerializeField] public GameObject techoNoche;

    [Header("Ventanas")]
    [SerializeField] public GameObject ventanaDia;
    [SerializeField] public GameObject ventanaTarde;
    [SerializeField] public GameObject ventanaNoche;

    [Header("Cortinas")]
    [SerializeField] public Animator animCortinaIzq;
    [SerializeField] public Animator animCortinaDer;

    [Header("Ventanas Corredizas")]
    [SerializeField] public Animator animVentanaIzq;
    [SerializeField] public Animator animVentanaDer;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private int contadorVentanas;

    private bool cortinaDerCerrada = false;
    private bool cortinaIzqCerrada = false;

    private bool ventanaIzqCerrada = false;
    private bool ventanaDerCerrada = false;

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;

        techoDia.SetActive(true);
        techoTarde.SetActive(false);
        techoNoche.SetActive(false);
        ventanaDia.SetActive(true);
        ventanaTarde.SetActive(false);
        ventanaNoche.SetActive(false);
    }

    void Update()
    {
        
        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria /*&& estaEnAreaDeInteraccion*/)
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

                    //Debug.Log(highlight.gameObject);

                    string objetoSeleccionado = highlight.gameObject.name;


                    //Se interactua con las cortinas y se cierran ambas y suman +1 al contador de ventanas, segun el numero es la hora del dia y si llega a 3 vuelve al inicio
                    switch (objetoSeleccionado)
                    {
                        case "CortinaDer":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.sonidoCortina);

                            if (cortinaDerCerrada == false)
                            {
                                animCortinaDer.SetBool("cerrarCortina", true);
                                cortinaDerCerrada = true;

                                StartCoroutine(CambiarCielo());
                            }
                            else if (cortinaDerCerrada == true)
                            {
                                animCortinaDer.SetBool("cerrarCortina", false);
                                cortinaDerCerrada = false;
                            }
                            break;

                        case "CortinaIzq":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.sonidoCortina);

                            if (cortinaIzqCerrada == false)
                            {
                                animCortinaIzq.SetBool("cerrarCortina", true);
                                cortinaIzqCerrada = true;

                                StartCoroutine(CambiarCielo());
                            }
                            else if (cortinaDerCerrada == true)
                            {
                                animCortinaIzq.SetBool("cerrarCortina", false);
                                cortinaIzqCerrada = false;
                            }
                            break;

                        case "VentanaIzq":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.sonidoVentana);

                            if (ventanaIzqCerrada == false)
                            {
                                animVentanaIzq.SetBool("abrirVentana", true);
                                ventanaIzqCerrada = true;
                            }
                            else if (ventanaIzqCerrada == true)
                            {
                                animVentanaIzq.SetBool("abrirVentana", false);
                                ventanaIzqCerrada = false;
                            }
                            break;

                        case "VentanaDer":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.sonidoVentana);

                            if (ventanaDerCerrada == false)
                            {
                                animVentanaDer.SetBool("abrirVentana", true);
                                ventanaDerCerrada = true;
                            }
                            else if (ventanaDerCerrada == true)
                            {
                                animVentanaDer.SetBool("abrirVentana", false);
                                ventanaDerCerrada = false;
                            }
                            break;

                        default:
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

    IEnumerator CambiarCielo()
    {
        if (cortinaDerCerrada == true && cortinaIzqCerrada == true)
        {
            contadorVentanas += 1;

            yield return new WaitForSeconds(1f);

            if (contadorVentanas == 1)
            {
                techoDia.SetActive(false);
                techoTarde.SetActive(true);
                ventanaDia.SetActive(false);
                ventanaTarde.SetActive(true);
            }
            else if (contadorVentanas == 2)
            {
                techoTarde.SetActive(false);
                techoNoche.SetActive(true);
                ventanaTarde.SetActive(false);
                ventanaNoche.SetActive(true);
            }
            else if (contadorVentanas >= 3)
            {
                techoNoche.SetActive(false);
                techoDia.SetActive(true);
                ventanaNoche.SetActive(false);
                ventanaDia.SetActive(true);
                contadorVentanas = 0;
            }
        }
    }
}
