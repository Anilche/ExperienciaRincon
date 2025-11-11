using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntDisruptiva : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool estaEnAreaDeInteraccion = false;

    //private string tagSeleccionable = "Seleccionable";

    [Header("Panel")]
    [SerializeField] public GameObject panelLuces;
    [SerializeField] public GameObject panelLucesDesconectado;
    [SerializeField] public GameObject panelLucesConectado;

    [Header("Animator Palanquitas")]
    [SerializeField] public Animator animPalanquita1;
    [SerializeField] public Animator animPalanquita2;
    [SerializeField] public Animator animPalanquita3;
    [SerializeField] public Animator animPalanquita4;
    [SerializeField] public Animator animPalanquita5;

    [Header("Luces Rojas")]
    [SerializeField] public GameObject luzRojaPalanquita1;
    [SerializeField] public GameObject luzRojaPalanquita2;
    [SerializeField] public GameObject luzRojaPalanquita3;
    [SerializeField] public GameObject luzRojaPalanquita4;
    [SerializeField] public GameObject luzRojaPalanquita5;

    [Header("Luces Verdes")]
    [SerializeField] public GameObject luzVerdePalanquita1;
    [SerializeField] public GameObject luzVerdePalanquita2;
    [SerializeField] public GameObject luzVerdePalanquita3;
    [SerializeField] public GameObject luzVerdePalanquita4;
    [SerializeField] public GameObject luzVerdePalanquita5;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private bool panelActivado = false;

    private bool palanquita1Arriba = false;
    private bool palanquita2Arriba = true;
    private bool palanquita3Arriba = true;
    private bool palanquita4Arriba = true;
    private bool palanquita5Arriba = true;

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }


    void Update()
    {
        if (palanquita1Arriba == true && palanquita2Arriba == true && palanquita3Arriba == false && palanquita4Arriba == true && palanquita5Arriba == false && GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            panelActivado = true;
            panelLucesDesconectado.SetActive(false);
            panelLucesConectado.SetActive(true);

            //Animacion luces de vuelta

            GameManager.GetInstance().SetFaseActual(1);
        }

        if (palanquita1Arriba == true)
        {
            luzRojaPalanquita1.SetActive(false);
            luzVerdePalanquita1.SetActive(true);
        }
        else
        {
            luzRojaPalanquita1.SetActive(true);
            luzVerdePalanquita1.SetActive(false);
        }

        if (palanquita2Arriba == true)
        {
            luzRojaPalanquita2.SetActive(false);
            luzVerdePalanquita2.SetActive(true);
        }
        else
        {
            luzRojaPalanquita2.SetActive(true);
            luzVerdePalanquita2.SetActive(false);
        }

        if (palanquita3Arriba == false)
        {
            luzRojaPalanquita3.SetActive(false);
            luzVerdePalanquita3.SetActive(true);
        }
        else
        {
            luzRojaPalanquita3.SetActive(true);
            luzVerdePalanquita3.SetActive(false);
        }

        if (palanquita4Arriba == true)
        {
            luzRojaPalanquita4.SetActive(false);
            luzVerdePalanquita4.SetActive(true);
        }
        else
        {
            luzRojaPalanquita4.SetActive(true);
            luzVerdePalanquita4.SetActive(false);
        }

        if (palanquita5Arriba == false)
        {
            luzRojaPalanquita5.SetActive(false);
            luzVerdePalanquita5.SetActive(true);
        }
        else
        {
            luzRojaPalanquita5.SetActive(true);
            luzVerdePalanquita5.SetActive(false);
        }



        if (GameManager.GetInstance().faseAhora == numFaseNecesaria && estaEnAreaDeInteraccion && panelActivado == false)
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

                    Debug.Log(highlight.gameObject);

                    string objetoSeleccionado = highlight.gameObject.name;

                    switch (objetoSeleccionado)
                    {
                        case "Palanquita1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            palanquita1Arriba = !palanquita1Arriba;

                            animPalanquita1.SetBool("PalanquitaArriba", palanquita1Arriba);

                            Debug.Log("Palanquita1");
                            break;

                        case "Palanquita2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            palanquita2Arriba = !palanquita2Arriba;
                            animPalanquita2.SetBool("PalanquitaArriba", palanquita2Arriba);

                            Debug.Log("Palanquita2");
                            break;

                        case "Palanquita3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            palanquita3Arriba = !palanquita3Arriba;
                            animPalanquita3.SetBool("PalanquitaArriba", palanquita3Arriba);

                            Debug.Log("Palanquita3");
                            break;

                        case "Palanquita4":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            palanquita4Arriba = !palanquita4Arriba;
                            animPalanquita4.SetBool("PalanquitaArriba", palanquita4Arriba);

                            Debug.Log("Palanquita4");
                            break;

                        case "Palanquita5":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            palanquita5Arriba = !palanquita5Arriba;
                            animPalanquita5.SetBool("PalanquitaArriba", palanquita5Arriba);

                            Debug.Log("Palanquita5");
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
            Debug.Log("Jugador en area de interaccion juegos");
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
            Debug.Log("Jugador salio del area de interaccion juegos");
        }
    }
}