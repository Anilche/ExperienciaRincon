using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelectionE2 : MonoBehaviour
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

    [Header("Animator Botones")]
    // Los objetos que se pueden elegir
    [SerializeField] public Animator animBoton1;
    [SerializeField] public Animator animBoton2;
    [SerializeField] public Animator animBoton3;
    [SerializeField] public Animator animBotonConfirmar;

    [Header("Pantallas")]
    // Pantallas que tendran animacion luego
    [SerializeField] public GameObject pantalla1;
    [SerializeField] public GameObject pantalla2;
    [SerializeField] public GameObject pantalla3;
    [SerializeField] public Animator animPantalla1;
    [SerializeField] public Animator animPantalla2;
    [SerializeField] public Animator animPantalla3;

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Tablero")]
    [SerializeField] public GameObject tablero2;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private void Awake()
    {
        objeto1.SetActive(false); // Desactiva el objeto 1 al inicio
        objeto2.SetActive(false); // Desactiva el objeto 2 al inicio
        objeto3.SetActive(false); // Desactiva el objeto 3 al inicio
        pantalla1.SetActive(false); // Desactiva la pantalla 1 al inicio
        pantalla2.SetActive(false); // Desactiva la pantalla 2 al inicio
        pantalla3.SetActive(false); // Desactiva la pantalla 3 al inicio
        particulas.SetActive(false); // Desactiva las particulas al inicio
    }

    void Update()
    {
        
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            //particulas.SetActive(true);

            pantalla1.SetActive(true);
            pantalla2.SetActive(true);
            pantalla3.SetActive(true);
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

                                selection.gameObject.GetComponent<Outline>().enabled = false;
                                animBoton1.SetTrigger("pulsarBoton");
                                break;

                            case "BotonPortal2":
                                objeto1.SetActive(false);
                                objeto2.SetActive(true);
                                objeto3.SetActive(false);

                                selection.gameObject.GetComponent<Outline>().enabled = false;
                                animBoton2.SetTrigger("pulsarBoton");
                                break;

                            case "BotonPortal3":
                                objeto1.SetActive(false);
                                objeto2.SetActive(false);
                                objeto3.SetActive(true);

                                selection.gameObject.GetComponent<Outline>().enabled = false;
                                animBoton3.SetTrigger("pulsarBoton");
                                break;

                            case "BotonConfirmar":
                                Debug.Log("Eleccion Confirmada");
                                GameManager.GetInstance().SetFaseActual(1);
                                selection.gameObject.GetComponent<Outline>().enabled = false; //Se deselecciona el boton confirmar

                                particulas.SetActive(false);

                                animBotonConfirmar.SetTrigger("pulsarBoton");

                                //Animaciones de salida de los portales/tablero
                                StartCoroutine(DesactivarObjetosDespuesDeAnimacion());
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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            estaEnAreaDeElecciones = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaEnAreaDeElecciones = false;
        }
    }

    IEnumerator DesactivarObjetosDespuesDeAnimacion()
    {
        animPantalla1.SetBool("AnimacionSalida", true);
        animPantalla2.SetBool("AnimacionSalida", true);
        animPantalla3.SetBool("AnimacionSalida", true);
        yield return new WaitForSeconds(3f); // Espera 3 segundos (ajustar el tiempo a la duracion de la animacion)
        // Desactiva los objetos despues de la animacion
        pantalla1.SetActive(false);
        pantalla2.SetActive(false);
        pantalla3.SetActive(false);
    }
}