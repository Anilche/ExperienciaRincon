using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteraccionesBonus : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Animacion The Notebook")]
    [SerializeField] private Animator animNotebook;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private Camera camara;

    private bool animacionEnCurso = false;

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
                        case "cuadro notebook_1":

                            if (animacionEnCurso == false)
                            {
                                selection.gameObject.GetComponent<Outline>().enabled = false;

                                audioManager.PlaySFX(audioManager.seleccionSFX);

                                StartCoroutine(HacerAnimacion());
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

    IEnumerator HacerAnimacion()
    {
        animacionEnCurso = true;
        animNotebook.SetBool("parpadearLuces", true);
        yield return new WaitForSeconds(0.5f);
        animNotebook.SetBool("parpadearLuces", false);
        animacionEnCurso = false;
    }
}
