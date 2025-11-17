using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstDescanso : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    // private string tagSeleccionable = "Seleccionable";
    [Header("Bandej")]
    [SerializeField] public Animator animBandeja;
    [SerializeField] public GameObject bandeja;

    [Header("Bebidas")]
    [SerializeField] public GameObject bebidas;
    [SerializeField] public GameObject cafe;
    [SerializeField] public GameObject te;
    [SerializeField] public GameObject mate;

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

        bebidas.SetActive(false);
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria /*&& estaEnAreaDeInteraccion*/)
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

                    switch (objetoSeleccionado)
                    {
                        case "Mate":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            Debug.Log("Seleccionaste el mate");
                            GameManager.GetInstance().faseAhora += 1;
                            StartCoroutine(hacerAnimacion(mate));

                            break;

                        case "Cafe":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            Debug.Log("Seleccionaste el café");
                            GameManager.GetInstance().faseAhora += 1;
                            StartCoroutine(hacerAnimacion(cafe));
                            break;

                        case "Te":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            Debug.Log("Seleccionaste el té");
                            GameManager.GetInstance().faseAhora += 1;
                            StartCoroutine(hacerAnimacion(te));
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
    
    IEnumerator hacerAnimacion(GameObject bebidaAElegir)
    {
        bebidas.SetActive(true);
        te.SetActive(false);
        mate.SetActive(false);
        cafe.SetActive(false);
        bebidaAElegir.SetActive(true);

        yield return new WaitForSeconds(1f);

        if (bebidaAElegir.name == "Te" || bebidaAElegir.name == "Cafe")
        {
            audioManager.PlaySFX(audioManager.sonidoTeCafe);
        }
        if (bebidaAElegir.name == "Mate")
        {
            audioManager.PlaySFX(audioManager.sonidoMate);
        }

        yield return new WaitForSeconds(2f);
        bebidas.SetActive(false);

        animBandeja.SetBool("Salida", true);
        yield return new WaitForSeconds(2f);
        bandeja.SetActive(false);
    }
}