using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inst4Spinetta : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    //private string tagSeleccionable = "Seleccionable";

    [Header("Animator Instrumento")]
    [SerializeField] public Animator animInstrumento;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Pantallas")]
    [SerializeField] public GameObject pantallaApagada;
    [SerializeField] public GameObject pantallaVideo;

    private Camera camara;

    private bool puedeTocar = true;

    private bool telePrendida = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria)
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

                    string objetoSeleccionado = highlight.gameObject.name;

                    switch (objetoSeleccionado)
                    {
                        case "Guitarra":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            if (puedeTocar)
                            {
                                StartCoroutine(hacerAnimacion());
                            }
                            break;

                        case "TV":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            if (telePrendida)
                            {
                                audioManager.PlaySFX(audioManager.sonidoTV);
                                pantallaApagada.SetActive(true);
                                pantallaVideo.SetActive(false);
                                telePrendida = false;
                            } else if (!telePrendida)
                            {
                                audioManager.PlaySFX(audioManager.sonidoTV);
                                pantallaApagada.SetActive(false);
                                pantallaVideo.SetActive(true);
                                telePrendida = true;
                            }
                            break;

                        case "Microfono":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            if (puedeTocar)
                            {
                                StartCoroutine(tocarMicrofono());
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

    IEnumerator hacerAnimacion()
    {
        puedeTocar = false;
        audioManager.PlaySFX(audioManager.sonidoGuitarra);
        animInstrumento.SetBool("tocar", true);
        yield return new WaitForSeconds(1.5f);
        animInstrumento.SetBool("tocar", false);
        puedeTocar = true;
    }

    IEnumerator tocarMicrofono()
    {
        puedeTocar = false;
        audioManager.PlaySFX(audioManager.sonidoMicrofono);
        yield return new WaitForSeconds(7f);
        puedeTocar = true;
    }
}
