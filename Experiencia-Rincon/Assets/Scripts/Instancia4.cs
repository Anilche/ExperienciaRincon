using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Instancia4 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool estaEnAreaDeElecciones = false;

    [Header("Musicas")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject musica1;
    [SerializeField] public GameObject musica2;
    [SerializeField] public GameObject musica3;

    [Header("Portadas")]
    // Pantallas que tendran animacion luego
    [SerializeField] public GameObject portada1;
    [SerializeField] public GameObject portada2;
    [SerializeField] public GameObject portada3;

    [Header("Discos")]
    // Pantallas que tendran animacion luego
    [SerializeField] public GameObject disco1;
    [SerializeField] public GameObject disco2;
    [SerializeField] public GameObject disco3;

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private void Awake()
    {
        particulas.SetActive(false); // Desactiva las particulas al inicio
        disco1.SetActive(false);
        disco2.SetActive(false);
        disco3.SetActive(false);
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            particulas.SetActive(true);
        }
        else
        {
            particulas.SetActive(false);
        }

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria && estaEnAreaDeElecciones)
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
                if (highlight.CompareTag("Seleccionable") && highlight != selection)
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
                if (highlight)
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
                        case "Portada1":
                            disco1.SetActive(true);
                            disco2.SetActive(false);
                            disco3.SetActive(false);
                            //Musica1
                            break;

                        case "Portada2":
                            disco1.SetActive(false);
                            disco2.SetActive(true);
                            disco3.SetActive(false);
                            //Musica2
                            break;

                        case "Portada3":
                            disco1.SetActive(false);
                            disco2.SetActive(false);
                            disco3.SetActive(true);
                            //Musica3
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
            estaEnAreaDeElecciones = true;
            particulas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaEnAreaDeElecciones = false;
        }
    }
}
