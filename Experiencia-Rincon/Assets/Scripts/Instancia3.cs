using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.VisualScripting.Member;

public class Instancia3 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    //private bool estaEnAreaDeElecciones = false;

    [Header("Estanterias finales")]
    [SerializeField] public GameObject estanteriasFinales;
    [SerializeField] public GameObject lucesEstanteriasFinales;

    [Header("Objetos Estanteria Final")]
    [SerializeField] public GameObject objeto1;
    [SerializeField] public GameObject objeto2;
    [SerializeField] public GameObject objeto3;
    [SerializeField] public GameObject objeto4;
    [SerializeField] public GameObject objeto5;
    [SerializeField] public GameObject objeto6;
    [SerializeField] public GameObject objeto7;

    [Header("Anim Objetos Estanteria Final")]
    [SerializeField] public Animator animObjetosFinal;

    [Header("Estanteria Seleccion")]
    [SerializeField] public GameObject estanteriaSeleccion;
    [SerializeField] public Animator animEstanteriaSeleccion;

    [Header("Objetos Estanteria Seleccion")]
    [SerializeField] public GameObject objetoSeleccion1;
    [SerializeField] public GameObject objetoSeleccion2;
    [SerializeField] public GameObject objetoSeleccion3;
    [SerializeField] public GameObject objetoSeleccion4;
    [SerializeField] public GameObject objetoSeleccion5;
    [SerializeField] public GameObject objetoSeleccion6;
    [SerializeField] public GameObject objetoSeleccion7;

    [Header("Objetos Extras Confirmacion")]
    [SerializeField] public GameObject objsExtras;

    [Header("Objeto Terminar Elecciones")]
    [SerializeField] public GameObject objTerminar;
    
    [Header("Animator Terminar Elecciones")]
    [SerializeField] public Animator animObjTerminar;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    private string noSeleccionable = "Untagged";

    private bool puedeConfirmar = false;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(false);
        objeto4.SetActive(false);
        objeto5.SetActive(false);
        objeto6.SetActive(false);
        objeto7.SetActive(false);
        objsExtras.SetActive(false);
        lucesEstanteriasFinales.SetActive(false);

        //estanteriaSeleccion.SetActive(false);
        //estanteriasFinales.SetActive(false);
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            objetoSeleccion1.tag = "Seleccionable";
            objetoSeleccion2.tag = "Seleccionable";
            objetoSeleccion3.tag = "Seleccionable";
            objetoSeleccion4.tag = "Seleccionable";
            objetoSeleccion5.tag = "Seleccionable";
            objetoSeleccion6.tag = "Seleccionable";
            objetoSeleccion7.tag = "Seleccionable";

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
                        case "ObjSeleccion1":
                            objeto1.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjSeleccion2":
                            objeto2.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjSeleccion3":
                            objeto3.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjSeleccion4":
                            objeto4.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjSeleccion5":
                            objeto5.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjSeleccion6":
                            objeto6.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjSeleccion7":
                            objeto7.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            puedeConfirmar = true;
                            objTerminar.tag = "Seleccionable";
                            break;

                        case "ObjFinal1":
                            objeto1.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjFinal2":
                            objeto2.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjFinal3":
                            objeto3.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjFinal4":
                            objeto4.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjFinal5":
                            objeto5.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjFinal6":
                            objeto6.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjFinal7":
                            objeto7.SetActive(false);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "TerminarElecciones":

                            if (puedeConfirmar == true)
                            {
                                GameManager.GetInstance().SetFaseActual(1); // Cambia la fase

                                audioManager.PlaySFX(audioManager.confirmacionSFX);

                                objsExtras.SetActive(true);

                                lucesEstanteriasFinales.SetActive(true);

                                StartCoroutine(DesactivarObjetosDespuesDeAnimacion());

                                objeto1.tag = noSeleccionable;
                                objeto2.tag = noSeleccionable;
                                objeto3.tag = noSeleccionable;
                                objeto4.tag = noSeleccionable;
                                objeto5.tag = noSeleccionable;
                                objeto6.tag = noSeleccionable;
                                objeto7.tag = noSeleccionable;

                                selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
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

    IEnumerator DesactivarObjetosDespuesDeAnimacion()
    {
        animObjTerminar.SetBool("BotonPresionado", true);
        yield return new WaitForSeconds(1.5f);
        animObjetosFinal.SetBool("ObjsSeleccionados", true);
        animEstanteriaSeleccion.SetBool("AnimacionSalida", true);
        yield return new WaitForSeconds(3.5f); // Espera X segundos (ajustar el tiempo a la duracion de la animacion)
        // Desactiva los objetos despues de la animacion
        estanteriaSeleccion.SetActive(false);
    }
}
