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
    [SerializeField] public Animator cortinaIzq;
    [SerializeField] public Animator cortinaDer;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private int contadorVentanas;

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }


    void Update()
    {
        
        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria /*&& estaEnAreaDeInteraccion*/)
        {
            //botonComida1.tag = tagSeleccionable;
            //botonComida2.tag = tagSeleccionable;
            //botonComida3.tag = tagSeleccionable;

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


                    //Se interactua con las cortinas y se cierran ambas y suman +1 al contador de ventanas, segun el numero es la hora del dia y si llega a 3 vuelve al inicio
                    switch (objetoSeleccionado)
                    {
                        case "BotonPizza":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            break;

                        case "BotonTarta":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

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

    IEnumerator animacionComida(GameObject comida1, GameObject comida2, GameObject comida3)
    {
        audioManager.PlaySFX(audioManager.sonidoMordida);
        comida1.SetActive(false);
        comida2.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySFX(audioManager.sonidoMordida);
        comida2.SetActive(false);
        comida3.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySFX(audioManager.sonidoMordida);
        comida3.SetActive(false);
    }
}
