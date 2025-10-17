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

    [Header("Opciones de Pisos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject piso1;
    [SerializeField] public GameObject piso2;
    [SerializeField] public GameObject piso3;
    [SerializeField] public GameObject pisoBase;

    [Header("Objetos Extras")]
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

    [Header("Portales")]
    // Portales que tendran animacion luego
    [SerializeField] public GameObject portal1;
    [SerializeField] public GameObject portal2;
    [SerializeField] public GameObject portal3;
    [SerializeField] public Animator animPortal1;
    [SerializeField] public Animator animPortal2;
    [SerializeField] public Animator animPortal3;

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Niebla")]
    [SerializeField] public GameObject niebla;
    [SerializeField] private Animator animControllerNiebla;

    [Header("Parpados")]
    [SerializeField] public GameObject parpados;

    [Header("Tablero y Base")]
    [SerializeField] public GameObject tableroYBase;

    [Header("Spotlights")]
    [SerializeField] public GameObject spotlightBase;
    [SerializeField] public GameObject spotlightPortal1;
    [SerializeField] public GameObject spotlightPortal2;
    [SerializeField] public GameObject spotlightPortal3;
    [SerializeField] public Animator animSpotlight1;
    [SerializeField] public Animator animSpotlight2;
    [SerializeField] public Animator animSpotlight3;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private void Awake()
    {
        objeto1.SetActive(false); // Desactiva el objeto 1 al inicio
        objeto2.SetActive(false); // Desactiva el objeto 2 al inicio
        objeto3.SetActive(false); // Desactiva el objeto 3 al inicio
        particulas.SetActive(false); // Desactiva las particulas al inicio

        portal1.SetActive(false); // desactiva el portal 1 al inicio
        portal2.SetActive(false);
        portal3.SetActive(false);

        tableroYBase.SetActive(false);
        spotlightBase.SetActive(false);
        spotlightPortal1.SetActive(false);
        spotlightPortal2.SetActive(false);
        spotlightPortal3.SetActive(false);
    }

    void Update()
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            //particulas.SetActive(true);

            //Animacion de entrada de los portales
            portal1.SetActive(true); //Activa el portal 1
            portal2.SetActive(true);
            portal3.SetActive(true);

            tableroYBase.SetActive(true);
            spotlightBase.SetActive(true);
            spotlightPortal1.SetActive(true);
            spotlightPortal2.SetActive(true);
            spotlightPortal3.SetActive(true);
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
                                /*objeto1.SetActive(true);
                                objeto2.SetActive(false);
                                objeto3.SetActive(false);

                                piso1.SetActive(true);
                                piso2.SetActive(false);
                                piso3.SetActive(false);
                                pisoBase.SetActive(false);*/

                                selection.gameObject.GetComponent<Outline>().enabled = false;
                                animBoton1.SetTrigger("pulsarBoton");

                                StartCoroutine(parpadearYCambiar(piso1, objeto1)); //Inicia la corutina para parpadear y cambiar de piso, se le pasa por parámetro el piso a elegir
                                break;

                            case "BotonPortal2":
                                /*objeto1.SetActive(false);
                                objeto2.SetActive(true);
                                objeto3.SetActive(false);*/

                                selection.gameObject.GetComponent<Outline>().enabled = false;
                                animBoton2.SetTrigger("pulsarBoton");

                                StartCoroutine(parpadearYCambiar(piso2, objeto2)); //Inicia la corutina para parpadear y cambiar de piso, se le pasa por parámetro el piso a elegir
                                break;

                            case "BotonPortal3":
                                /*objeto1.SetActive(false);
                                objeto2.SetActive(false);
                                objeto3.SetActive(true);*/

                                selection.gameObject.GetComponent<Outline>().enabled = false;
                                animBoton3.SetTrigger("pulsarBoton");

                                StartCoroutine(parpadearYCambiar(piso3, objeto3)); //Inicia la corutina para parpadear y cambiar de piso, se le pasa por parámetro el piso a elegir
                                break;

                            case "BotonConfirmar":
                                Debug.Log("Eleccion Confirmada");
                                GameManager.GetInstance().SetFaseActual(1);
                                selection.gameObject.GetComponent<Outline>().enabled = false; //Se deselecciona el boton confirmar
                                
                                particulas.SetActive(false);
                                
                                animBotonConfirmar.SetTrigger("pulsarBoton");

                                //Animaciones de salida de niebla, portales y spotlights
                                animControllerNiebla.SetBool("bajarNiebla", true); //Animacion de salida de la niebla
                                StartCoroutine(DesactivarObjetosDespuesDeAnimacion()); //Inicia la corutina para desactivar los objetos luego de la animacion
                                
                                //Añadir sonido de confirmacion
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

    IEnumerator parpadearYCambiar(GameObject pisoAElegir, GameObject objetosExtras) { 
        parpados.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        piso1.SetActive(false);
        piso2.SetActive(false);
        piso3.SetActive(false);
        pisoAElegir.SetActive(true);
        pisoBase.SetActive(false);

        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(false);
        objetosExtras.SetActive(true);

        yield return new WaitForSeconds(1f);
        parpados.SetActive(false);
    }

    IEnumerator DesactivarObjetosDespuesDeAnimacion()
    {
        animSpotlight1.SetBool("AnimacionSalida", true);
        animSpotlight2.SetBool("AnimacionSalida", true);
        animSpotlight3.SetBool("AnimacionSalida", true);
        animPortal1.SetBool("AnimacionSalida", true);
        animPortal2.SetBool("AnimacionSalida", true);
        animPortal3.SetBool("AnimacionSalida", true);
        yield return new WaitForSeconds(3f); // Espera 3 segundos (ajustar el tiempo a la duracion de la animacion)
        // Desactiva los objetos despues de la animacion
        portal1.SetActive(false);
        portal2.SetActive(false);
        portal3.SetActive(false);
        spotlightPortal1.SetActive(false);
        spotlightPortal2.SetActive(false);
        spotlightPortal3.SetActive(false);
        niebla.SetActive(false);
    }
}