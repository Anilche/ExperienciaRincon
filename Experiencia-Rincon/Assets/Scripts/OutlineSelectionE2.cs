using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelectionE2 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private string tagNoSeleccionable = "Untagged";

    [Header("Opciones de Objetos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject objeto1;
    [SerializeField] public GameObject objeto2;
    [SerializeField] public GameObject objeto3;

    [Header("Opciones de Paredes")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject paredBase;
    [SerializeField] public GameObject pared1;
    [SerializeField] public GameObject pared2;
    [SerializeField] public GameObject pared3;

    [Header("Opciones de Techos")]
    // Los objetos que se pueden elegir
    [SerializeField] public GameObject techoBase;
    [SerializeField] public GameObject techo1;
    [SerializeField] public GameObject techo2;
    [SerializeField] public GameObject techo3;

    [Header("Animator Botones")]
    // Los objetos que se pueden elegir
    [SerializeField] public Animator animBoton1;
    [SerializeField] public Animator animBoton2;
    [SerializeField] public Animator animBoton3;
    [SerializeField] public Animator animBotonConfirmar;

    [Header("Botones")]
    [SerializeField] public GameObject boton1;
    [SerializeField] public GameObject boton2;
    [SerializeField] public GameObject boton3;
    [SerializeField] public GameObject botonConfirmar;

    [Header("Pantallas")]
    // Pantallas que tendran animacion luego
    [SerializeField] public GameObject pantalla1;
    [SerializeField] public GameObject pantalla2;
    [SerializeField] public GameObject pantalla3;
    [SerializeField] public Animator animPantalla1;
    [SerializeField] public Animator animPantalla2;
    [SerializeField] public Animator animPantalla3;

    [Header("Animator Tablero")]
    [SerializeField] public Animator animTablero;

    [Header("Transiciones")]
    [SerializeField] public GameObject transPelotas;
    [SerializeField] public GameObject transMedialunas;
    [SerializeField] public GameObject transMonedas;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    private bool puedeConfirmar = false;

    private bool puedeTocar = true;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        objeto1.SetActive(false); // Desactiva el objeto 1 al inicio
        objeto2.SetActive(false); // Desactiva el objeto 2 al inicio
        objeto3.SetActive(false); // Desactiva el objeto 3 al inicio
        pantalla1.SetActive(false); // Desactiva la pantalla 1 al inicio
        pantalla2.SetActive(false); // Desactiva la pantalla 2 al inicio
        pantalla3.SetActive(false); // Desactiva la pantalla 3 al inicio
    }

    void Update()
    {
        
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            pantalla1.SetActive(true);
            pantalla2.SetActive(true);
            pantalla3.SetActive(true);
        }

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
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
                            case "BotonPantalla1":

                                if (puedeTocar)
                                {
                                    selection.gameObject.GetComponent<Outline>().enabled = false;

                                    animBoton1.SetTrigger("pulsarBoton");

                                    audioManager.PlaySFX(audioManager.seleccionSFX);

                                    StartCoroutine(transicionYCambiar(transPelotas, pared1, techo1, objeto1));
                                    puedeConfirmar = true;

                                    botonConfirmar.tag = "Seleccionable";
                                }
                                break;

                            case "BotonPantalla2":

                                if (puedeTocar)
                                {
                                    selection.gameObject.GetComponent<Outline>().enabled = false;

                                    animBoton2.SetTrigger("pulsarBoton");

                                    audioManager.PlaySFX(audioManager.seleccionSFX);

                                    StartCoroutine(transicionYCambiar(transMedialunas, pared2, techo2, objeto2));
                                    puedeConfirmar = true;

                                    botonConfirmar.tag = "Seleccionable";
                                }
                                break;

                            case "BotonPantalla3":

                                if (puedeTocar)
                                {
                                    selection.gameObject.GetComponent<Outline>().enabled = false;

                                    animBoton3.SetTrigger("pulsarBoton");

                                    audioManager.PlaySFX(audioManager.seleccionSFX);

                                    StartCoroutine(transicionYCambiar(transMonedas, pared3, techo3, objeto3));
                                    puedeConfirmar = true;

                                    botonConfirmar.tag = "Seleccionable";
                                }
                                break;

                            case "BotonConfirmar":

                                if (puedeConfirmar == true)
                                {
                                    GameManager.GetInstance().SetFaseActual(1);
                                    selection.gameObject.GetComponent<Outline>().enabled = false; //Se deselecciona el boton confirmar

                                    audioManager.PlaySFX(audioManager.confirmacionSFX); //Efecto de sonido

                                    botonConfirmar.tag = tagNoSeleccionable;
                                    boton1.tag = tagNoSeleccionable;
                                    boton2.tag = tagNoSeleccionable;
                                    boton3.tag = tagNoSeleccionable;

                                    StartCoroutine(DesactivarObjetosDespuesDeAnimacion()); //Animaciones de salida de los portales/tablero
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

    IEnumerator transicionYCambiar(GameObject transicion, GameObject paredAElegir, GameObject techoAElegir, GameObject objetosExtras)
    {
        puedeTocar = false; // Evita que se puedan tocar más botones mientras se realiza la animación
        transicion.SetActive(false);
        transicion.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        techo1.SetActive(false);
        techo2.SetActive(false);
        techo3.SetActive(false);
        techoAElegir.SetActive(true);
        techoBase.SetActive(false);

        pared1.SetActive(false);
        pared2.SetActive(false);
        pared3.SetActive(false);
        paredAElegir.SetActive(true);
        paredBase.SetActive(false);

        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(false);
        objetosExtras.SetActive(true);

        yield return new WaitForSeconds(1f);
        transicion.SetActive(false);
        puedeTocar = true; // Vuelve a permitir tocar botones
    }

    IEnumerator DesactivarObjetosDespuesDeAnimacion()
    {
        animBotonConfirmar.SetTrigger("pulsarBoton");
        animPantalla1.SetBool("AnimacionSalida", true);
        animPantalla2.SetBool("AnimacionSalida", true);
        animPantalla3.SetBool("AnimacionSalida", true);
        animTablero.SetBool("SalidaTablero", true);
        yield return new WaitForSeconds(4f); // Espera 3 segundos (ajustar el tiempo a la duracion de la animacion)
        // Desactiva los objetos despues de la animacion
        pantalla1.SetActive(false);
        pantalla2.SetActive(false);
        pantalla3.SetActive(false);
        Destroy(this.gameObject);
    }
}