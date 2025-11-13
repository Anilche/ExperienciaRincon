using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inst2Menu : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private string tagSeleccionable = "Seleccionable";

    [Header("Botones Comidas")]
    [SerializeField] public GameObject botonComida1;
    [SerializeField] public GameObject botonComida2;
    [SerializeField] public GameObject botonComida3;

    [Header("Comidas")]
    [SerializeField] public GameObject pizza1;
    [SerializeField] public GameObject pizza2;
    [SerializeField] public GameObject pizza3;

    [SerializeField] public GameObject tarta1;
    [SerializeField] public GameObject tarta2;
    [SerializeField] public GameObject tarta3;

    [SerializeField] public GameObject empanadas1;
    [SerializeField] public GameObject empanadas2;
    [SerializeField] public GameObject empanadas3;

    [SerializeField] public GameObject migajas;


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

        pizza1.SetActive(false);
        pizza2.SetActive(false);
        pizza3.SetActive(false);
        tarta1.SetActive(false);
        tarta2.SetActive(false);
        tarta3.SetActive(false);
        empanadas1.SetActive(false);
        empanadas2.SetActive(false);
        empanadas3.SetActive(false);
        migajas.SetActive(false);
    }


    void Update()
    {

        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria /*&& estaEnAreaDeInteraccion*/)
        {
            botonComida1.tag = tagSeleccionable;
            botonComida2.tag = tagSeleccionable;
            botonComida3.tag = tagSeleccionable;

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
                        case "BotonPizza":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            botonComida1.SetActive(false);
                            botonComida2.SetActive(false);
                            botonComida3.SetActive(false);

                            //Sonido para captar la atencion en la mesa
                            audioManager.PlaySFX(audioManager.sonidoCampanaComida);

                            pizza1.SetActive(true);
                            break;

                        case "BotonTarta":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            botonComida1.SetActive(false);
                            botonComida2.SetActive(false);
                            botonComida3.SetActive(false);

                            //Sonido para captar la atencion en la mesa
                            audioManager.PlaySFX(audioManager.sonidoCampanaComida);

                            tarta1.SetActive(true);
                            break;

                        case "BotonEmpanadas":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            botonComida1.SetActive(false);
                            botonComida2.SetActive(false);
                            botonComida3.SetActive(false);

                            //Sonido para captar la atencion en la mesa
                            audioManager.PlaySFX(audioManager.sonidoCampanaComida);

                            empanadas1.SetActive(true);
                            break;

                        case "Pizza1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            StartCoroutine(animacionComida(pizza1, pizza2, pizza3));
                            break;

                        case "Tarta1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            StartCoroutine(animacionComida(tarta1, tarta2, tarta3));
                            break;

                        case "Empanadas1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            StartCoroutine(animacionComida(empanadas1, empanadas2, empanadas3));
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
        migajas.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySFX(audioManager.sonidoMordida);
        comida2.SetActive(false);
        comida3.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySFX(audioManager.sonidoMordida);
        comida3.SetActive(false);
    }
}