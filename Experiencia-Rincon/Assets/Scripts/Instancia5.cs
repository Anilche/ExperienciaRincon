using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Instancia5 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("Cuadros Tirados")]
    [SerializeField] public GameObject cuadroTirado1;
    [SerializeField] public GameObject cuadroTirado2;
    [SerializeField] public GameObject cuadroTirado3;

    [Header("Cuadros Colgando")]
    [SerializeField] public GameObject cuadroColgando1;
    [SerializeField] public GameObject cuadroColgando2;
    [SerializeField] public GameObject cuadroColgando3;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f;

    private int contadorCuadrosColocados = 0;

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }

    void Start()
    {
        cuadroColgando1.SetActive(false);
        cuadroColgando2.SetActive(false);
        cuadroColgando3.SetActive(false);
        cuadroTirado1.SetActive(true);
        cuadroTirado2.SetActive(true);
        cuadroTirado3.SetActive(true);
    }

    void Update()
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            if (contadorCuadrosColocados == 0)
            {
                cuadroColgando1.tag = "Seleccionable";
                cuadroColgando2.tag = "Seleccionable";
                cuadroColgando3.tag = "Seleccionable";
            }

            if (contadorCuadrosColocados == 3)
            {
                GameManager.GetInstance().SetFaseActual(numFaseNecesaria + 1);
            }

            // Highlight
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                highlight = raycastHit.transform;

                float distanciaHighlight = Vector3.Distance(
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    highlight.position
                );

                if (highlight.CompareTag("Seleccionable") && distanciaHighlight <= distanciaMaxima)
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

            // -------------------------
            //   SECCIÓN CORREGIDA
            // -------------------------
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Primero: validar que HIGHLIGHT no sea null
                if (highlight == null)
                    return;

                // Ahora que sabemos que highlight existe, medimos distancia
                float distancia = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position,highlight.position);

                if (distancia <= distanciaMaxima)
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
                        case "CuadroTirado1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;
                            cuadroTirado1.SetActive(false);
                            cuadroColgando1.SetActive(true);
                            break;

                        case "CuadroTirado2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;
                            cuadroTirado2.SetActive(false);
                            cuadroColgando2.SetActive(true);
                            break;

                        case "CuadroTirado3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;
                            cuadroTirado3.SetActive(false);
                            cuadroColgando3.SetActive(true);
                            break;

                        case "Cuadro1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;
                            cuadroColgando1.tag = "Untagged";
                            Debug.Log("Cuadro 1 seleccionado");
                            break;

                        case "Cuadro2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;
                            cuadroColgando2.tag = "Untagged";
                            Debug.Log("Cuadro 2 seleccionado");
                            break;

                        case "Cuadro3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;
                            cuadroColgando3.tag = "Untagged";
                            Debug.Log("Cuadro 3 seleccionado");
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