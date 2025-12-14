using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Instancia5 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("Animator Cuadros Tirados")]
    [SerializeField] public Animator cuadrosAnim;

    [Header("Cuadros Tirados")]
    [SerializeField] public GameObject cuadrosTirados;
    [SerializeField] public GameObject cuadroTirado1;
    [SerializeField] public GameObject cuadroTirado2;
    [SerializeField] public GameObject cuadroTirado3;

    [Header("Cuadros Colgando")]
    [SerializeField] public GameObject cuadroColgando1;
    [SerializeField] public GameObject cuadroColgando2;
    [SerializeField] public GameObject cuadroColgando3;

    [Header("Camaras")]
    [SerializeField] private CinemachineVirtualCamera vcamJugador;
    [SerializeField] private CinemachineVirtualCamera vcamLienzo1;
    [SerializeField] private CinemachineVirtualCamera vcamLienzo2;
    [SerializeField] private CinemachineVirtualCamera vcamLienzo3;

    [Header("UI Elecciones")]
    [SerializeField] private GameObject uiEleccionesLienzo1;
    [SerializeField] private GameObject uiEleccionesLienzo2;
    [SerializeField] private GameObject uiEleccionesLienzo3;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    private int contadorCuadrosColocados = 0;

    private string tagNoSeleccionable = "Untagged";

    private Camera camara;

    AudioManager audioManager;

    public string eleccionActiva = "";

    private bool inicioDeInstancia = true;

    private static Instancia5 instance;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
        instance = this;
    }

    void Start()
    {
        cuadroColgando1.SetActive(false);
        cuadroColgando2.SetActive(false);
        cuadroColgando3.SetActive(false);
        cuadrosTirados.SetActive(false);

        uiEleccionesLienzo1.SetActive(false);
        uiEleccionesLienzo2.SetActive(false);
        uiEleccionesLienzo3.SetActive(false);
    }

    void Update()
    {
        
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            if (inicioDeInstancia)
            {
                cuadroColgando1.tag = "Seleccionable";
                cuadroColgando2.tag = "Seleccionable";
                cuadroColgando3.tag = "Seleccionable";
            }

            cuadrosTirados.SetActive(true);

            if (contadorCuadrosColocados == 3)
            {
                GameManager.GetInstance().SetFaseActual(1);
                cuadroColgando1.tag = "Untagged";
                cuadroColgando2.tag = "Untagged";
                cuadroColgando3.tag = "Untagged";

                Destroy(this.gameObject);
            }

            if (!GameManager.GetInstance().eleccionActiva)
            {
                uiEleccionesLienzo1.SetActive(false);
                uiEleccionesLienzo2.SetActive(false);
                uiEleccionesLienzo3.SetActive(false);
            }

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
                        case "CuadroTirado1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);
                            cuadroTirado1.SetActive(false);
                            cuadroColgando1.SetActive(true);
                            break;

                        case "CuadroTirado2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);
                            cuadroTirado2.SetActive(false);
                            cuadroColgando2.SetActive(true);
                            break;

                        case "CuadroTirado3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);
                            cuadroTirado3.SetActive(false);
                            cuadroColgando3.SetActive(true);
                            break;

                        case "Cuadro1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            eleccionActiva = "Lienzo1";

                            IniciarEleccion(vcamLienzo1, uiEleccionesLienzo1);
                            break;

                        case "Cuadro2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            eleccionActiva = "Lienzo2";

                            IniciarEleccion(vcamLienzo2, uiEleccionesLienzo2);
                            break;

                        case "Cuadro3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            eleccionActiva = "Lienzo3";

                            IniciarEleccion(vcamLienzo3, uiEleccionesLienzo3);
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

    private void IniciarEleccion(CinemachineVirtualCamera vcamEleccion, GameObject uiElecciones)
    {
        GameManager.GetInstance().eleccionActiva = true; // Marca que la elección está activa

        inicioDeInstancia = false;

        Cursor.visible = true; // Hace visible el cursor
        Cursor.lockState = CursorLockMode.None;

        ActivarCamaraEleccion(vcamEleccion); // Activa la cámara de elecciones
        
        StartCoroutine(esperarYPrenderUI(uiElecciones));
    }

    public void ConfirmarSeleccion()
    {
        ActivarCamaraJugador();

        uiEleccionesLienzo1.SetActive(false);
        uiEleccionesLienzo2.SetActive(false);
        uiEleccionesLienzo3.SetActive(false);

        switch (eleccionActiva)
        {
            case "Lienzo1":
                cuadroColgando1.tag = tagNoSeleccionable;
                break;

            case "Lienzo2":
                cuadroColgando2.tag = tagNoSeleccionable;
                break;

            case "Lienzo3":
                cuadroColgando3.tag = tagNoSeleccionable;
                break;
        }

        GameManager.GetInstance().eleccionActiva = false; // Marca que la elección ya no está activa

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Oculta el cursor

        eleccionActiva = "";

        contadorCuadrosColocados++; // Aumenta el contador de cuadros colocados
    }

    private void ActivarCamaraJugador()
    {
        vcamJugador.Priority = 10;
        vcamLienzo1.Priority = 0;
        vcamLienzo2.Priority = 0;
        vcamLienzo3.Priority = 0;
    }

    private void ActivarCamaraEleccion(CinemachineVirtualCamera vcamEleccion)
    {
        vcamJugador.Priority = 0;
        vcamEleccion.Priority = 10;
    }

    IEnumerator esperarYPrenderUI(GameObject ui)
    {
        yield return new WaitForSeconds(2f);
        ui.SetActive(true);
    }

    public static Instancia5 GetInstance()
    {
        return instance;
    }
}
