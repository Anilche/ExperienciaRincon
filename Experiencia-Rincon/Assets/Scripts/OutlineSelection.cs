using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool estaEnAreaDeElecciones = false;

    [Header("Opciones de Objetos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject objeto1;
    [SerializeField] public GameObject objeto2;
    [SerializeField] public GameObject objeto3;

    [Header("Portales")]
    // Los portales que se pueden elegir
    [SerializeField] public GameObject portal1;
    [SerializeField] public GameObject portal2;
    [SerializeField] public GameObject portal3;

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Niebla")]
    [SerializeField] public GameObject niebla;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private void Awake()
    {
        objeto1.SetActive(false); // Desactiva el objeto 1 al inicio
        objeto2.SetActive(false); // Desactiva el objeto 2 al inicio
        objeto3.SetActive(false); // Desactiva el objeto 3 al inicio
        particulas.SetActive(false); // Desactiva las particulas al inicio
    }

    void Update()
    {
        
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            particulas.SetActive(true);
            //paredes.SetActive(true);
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
                            case "BotonPortal1":
                                objeto1.SetActive(true);
                                objeto2.SetActive(false);
                                objeto3.SetActive(false);
                                break;

                            case "BotonPortal2":
                                objeto1.SetActive(false);
                                objeto2.SetActive(true);
                                objeto3.SetActive(false);
                                break;

                            case "BotonPortal3":
                                objeto1.SetActive(false);
                                objeto2.SetActive(false);
                                objeto3.SetActive(true);
                                break;

                            case "BotonConfirmar":
                                Debug.Log("Eleccion Confirmada");
                                GameManager.GetInstance().SetFaseActual(1);
                                selection.gameObject.GetComponent<Outline>().enabled = false; //Se deselecciona el boton confirmar
                                //Animaciones de salida de los portales/tablero/base central
                                //Animacion de salida de la niebla
                                particulas.SetActive(false);
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