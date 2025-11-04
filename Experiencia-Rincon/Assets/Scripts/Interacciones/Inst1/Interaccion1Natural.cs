using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Interaccion1Natural : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool estaEnAreaDeInteraccion = false;

    private string tagSeleccionable = "Seleccionable";

    private int contadorManzanasRecogidas = 0;

    [Header("Manzanas")]
    [SerializeField] public GameObject manzana1;
    [SerializeField] public GameObject manzana2;
    [SerializeField] public GameObject manzana3;
    [SerializeField] public GameObject manzana4;
    [SerializeField] public GameObject manzana5;
    [SerializeField] public GameObject manzana6;

    [Header("Manzanas Canasta")]
    [SerializeField] public GameObject manzanaCanasta1;
    [SerializeField] public GameObject manzanaCanasta2;
    [SerializeField] public GameObject manzanaCanasta3;
    [SerializeField] public GameObject manzanaCanasta4;
    [SerializeField] public GameObject manzanaCanasta5;
    [SerializeField] public GameObject manzanaCanasta6;


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

        manzanaCanasta1.SetActive(false);
        manzanaCanasta2.SetActive(false);
        manzanaCanasta3.SetActive(false);
        manzanaCanasta4.SetActive(false);
        manzanaCanasta5.SetActive(false);
        manzanaCanasta6.SetActive(false);
    }

    void Update()
    {
        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            manzana1.GetComponent<Rigidbody>().isKinematic = false;
            manzana2.GetComponent<Rigidbody>().isKinematic = false;
            manzana3.GetComponent<Rigidbody>().isKinematic = false;
            manzana4.GetComponent<Rigidbody>().isKinematic = false;
            manzana5.GetComponent<Rigidbody>().isKinematic = false;
            manzana6.GetComponent<Rigidbody>().isKinematic = false;
        }
            
        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria && estaEnAreaDeInteraccion)
        {
            manzana1.tag = tagSeleccionable;
            manzana2.tag = tagSeleccionable;
            manzana3.tag = tagSeleccionable;
            manzana4.tag = tagSeleccionable;
            manzana5.tag = tagSeleccionable;
            manzana6.tag = tagSeleccionable;

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
                        case "Manzana1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            manzana1.SetActive(false);
                            manzanaCanasta1.SetActive(true);

                            contadorManzanasRecogidas++;
                            Debug.Log("Manzanas recogidas: " + contadorManzanasRecogidas);
                            break;

                        case "Manzana2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            manzana2.SetActive(false);
                            manzanaCanasta2.SetActive(true);

                            contadorManzanasRecogidas++;
                            Debug.Log("Manzanas recogidas: " + contadorManzanasRecogidas);
                            break;

                        case "Manzana3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            manzana3.SetActive(false);
                            manzanaCanasta3.SetActive(true);

                            contadorManzanasRecogidas++;
                            Debug.Log("Manzanas recogidas: " + contadorManzanasRecogidas);
                            break;

                        case "Manzana4":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            manzana4.SetActive(false);
                            manzanaCanasta4.SetActive(true);

                            contadorManzanasRecogidas++;
                            Debug.Log("Manzanas recogidas: " + contadorManzanasRecogidas);
                            break;

                        case "Manzana5":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            manzana5.SetActive(false);
                            manzanaCanasta5.SetActive(true);

                            contadorManzanasRecogidas++;
                            Debug.Log("Manzanas recogidas: " + contadorManzanasRecogidas);
                            break;

                        case "Manzana6":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            manzana6.SetActive(false);
                            manzanaCanasta6.SetActive(true);

                            contadorManzanasRecogidas++;
                            Debug.Log("Manzanas recogidas: " + contadorManzanasRecogidas);
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
            Debug.Log("Jugador en area de interaccion natural");
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
            Debug.Log("Jugador salio del area de interaccion natural");
        }
    }
}
