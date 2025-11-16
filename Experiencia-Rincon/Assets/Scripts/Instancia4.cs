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
    
    [Header("AudioSource")]
    public AudioSource Source;

    [Header("Mesa")]
    [SerializeField] public GameObject mesa;

    [Header("Audio Clips Musicas")]
    // Los audios que se pueden elegir
    [SerializeField] public AudioClip audioClipMusica1;
    [SerializeField] public AudioClip audioClipMusica2;
    [SerializeField] public AudioClip audioClipMusica3;
    
    [Header("Portadas")]
    [SerializeField] public GameObject portada1;
    [SerializeField] public GameObject portada2;
    [SerializeField] public GameObject portada3;

    [Header("Discos")]
    [SerializeField] public GameObject disco1;
    [SerializeField] public GameObject disco2;
    [SerializeField] public GameObject disco3;

    [Header("Objetos Extras")]
    [SerializeField] public GameObject objsSpinetta;
    [SerializeField] public GameObject objsTheW;
    [SerializeField] public GameObject objsJBIS;

    [Header("Animaciones")]
    [SerializeField] public Animator animatorTocadiscos;

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Audios")]
    [SerializeField] AudioClip audioClipSeleccion1;
    [SerializeField] AudioClip audioClipSeleccion2;
    [SerializeField] AudioClip audioClipSeleccion3;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        particulas.SetActive(false); // Desactiva las particulas al inicio
        disco1.SetActive(false);
        disco2.SetActive(false);
        disco3.SetActive(false);
        mesa.SetActive(false);

        objsSpinetta.SetActive(false);
        objsTheW.SetActive(false);
        objsJBIS.SetActive(false);
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            //particulas.SetActive(true);
            mesa.SetActive(true);
        }

        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria && estaEnAreaDeElecciones)
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

                            objsSpinetta.SetActive(true);
                            objsTheW.SetActive(false);
                            objsJBIS.SetActive(false);

                            //Cambio de musica
                            audioManager.ChangeMusic(audioClipSeleccion1);

                            particulas.SetActive(false);
                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar

                            cambiarFase();
                            Debug.Log("Fase actual: " + GameManager.GetInstance().faseAhora);

                            break;

                        case "Portada2":
                            disco1.SetActive(false);
                            disco2.SetActive(true);
                            disco3.SetActive(false);

                            objsSpinetta.SetActive(false);
                            objsTheW.SetActive(true);
                            objsJBIS.SetActive(false);

                            audioManager.ChangeMusic(audioClipSeleccion2);

                            particulas.SetActive(false);
                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar

                            cambiarFase();
                            Debug.Log("Fase actual: " + GameManager.GetInstance().faseAhora);

                            break;

                        case "Portada3":
                            disco1.SetActive(false);
                            disco2.SetActive(false);
                            disco3.SetActive(true);

                            objsSpinetta.SetActive(false);
                            objsTheW.SetActive(false);
                            objsJBIS.SetActive(true);

                            //Cambio de musica
                            audioManager.ChangeMusic(audioClipSeleccion3);

                            particulas.SetActive(false);
                            selection.gameObject.GetComponent<Outline>().enabled = false; // Quita el outline al seleccionar

                            cambiarFase();
                            Debug.Log("Fase actual: " + GameManager.GetInstance().faseAhora);

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

    private void cambiarFase()
    {
        if (GameManager.GetInstance().faseAhora == 10)
        {
            animatorTocadiscos.SetBool("CerrarTapa", true);
            GameManager.GetInstance().faseAhora += 1; // Cambia a la fase siguiente para que no se vuelva a llamar esta función
        }
    }
}
