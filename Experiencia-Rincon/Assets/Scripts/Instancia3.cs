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

    private bool estaEnAreaDeElecciones = false;

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


    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private string noSeleccionable = "Untagged";

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
        /*if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            //particulas.SetActive(true);

            //estanteriaSeleccion.SetActive(true);
            //estanteriasFinales.SetActive(true);
        }*/

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

                    //Debug.Log(highlight.gameObject);

                    string objetoSeleccionado = highlight.gameObject.name;

                    switch (objetoSeleccionado)
                    {
                        case "ObjSeleccion1":
                            objeto1.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjSeleccion2":
                            objeto2.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjSeleccion3":
                            objeto3.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjSeleccion4":
                            objeto4.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjSeleccion5":
                            objeto5.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjSeleccion6":
                            objeto6.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
                            break;

                        case "ObjSeleccion7":
                            objeto7.SetActive(true);

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar
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
                            GameManager.GetInstance().SetFaseActual(1); // Cambia la fase
                            
                            //animEstanteriaSeleccion.SetBool("AnimacionSalida", true);

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
        animObjTerminar.SetBool("BotonPresionado", true);
        yield return new WaitForSeconds(1.5f);
        animObjetosFinal.SetBool("ObjsSeleccionados", true);
        animEstanteriaSeleccion.SetBool("AnimacionSalida", true);
        yield return new WaitForSeconds(6f); // Espera X segundos (ajustar el tiempo a la duracion de la animacion)
        // Desactiva los objetos despues de la animacion
        estanteriaSeleccion.SetActive(false);
    }
}
