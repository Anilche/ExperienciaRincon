using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Instancia6 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("Pantalla")]
    [SerializeField] public GameObject pantallaSeleccion;
    [SerializeField] public GameObject pantallaListo;

    [Header("Botones Pantalla")]
    [SerializeField] public GameObject botonSeleccion1;
    [SerializeField] public GameObject botonSeleccion2;
    [SerializeField] public GameObject botonSeleccion3;
    [SerializeField] public GameObject botonSeleccionConfirmacion;

    [Header("Bases")]
    [SerializeField] public GameObject base1;
    [SerializeField] public GameObject base2;
    [SerializeField] public GameObject base3;

    [Header("Animators")]
    [SerializeField] public GameObject animBase1;
    [SerializeField] public GameObject animBase2;
    [SerializeField] public GameObject animBase3;
    [SerializeField] public GameObject animBaseInicio;


    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }

    void Start()
    {
        pantallaSeleccion.SetActive(false);
        base1.SetActive(false);
        base2.SetActive(false);
        base3.SetActive(false);
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            //cuadroColgando1.tag = "Seleccionable";
            //cuadroColgando2.tag = "Seleccionable";
            //cuadroColgando3.tag = "Seleccionable";

            pantallaSeleccion.SetActive(true);

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

                    string objetoSeleccionado = highlight.gameObject.name;

                    switch (objetoSeleccionado)
                    {
                        case "BotonHamburguesa":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            //Animacion salida base normal y entrada base hamburguesa
                            //Agregar que cuando se elija una opcion se oculte el objeto anterior (animator)
                            break;

                        case "BotonPizza":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            //Animacion salida base normal y entrada base pizza
                            break;

                        case "BotonTorta":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            //Animacion salida base normal y entrada base torta
                            break;

                        case "BotonConfirmacion":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.sonidoPedido);

                            //Aparece pantalla de listo y desaparecen los botones de seleccion

                            pantallaListo.SetActive(true);

                            botonSeleccion1.SetActive(false);
                            botonSeleccion2.SetActive(false);
                            botonSeleccion3.SetActive(false);
                            botonSeleccionConfirmacion.SetActive(false);
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
}