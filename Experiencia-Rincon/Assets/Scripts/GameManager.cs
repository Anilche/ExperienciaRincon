using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int faseAhora = 0; // Fase actual del jugador, se puede setear desde otro script

    public bool eleccionActiva;

    [SerializeField] public GameObject botonesInst1;
    [SerializeField] public GameObject botonesInst2;

    [SerializeField] public GameObject postProOscuro;

    [Header("Iluminacion Calido")]
    [SerializeField] public GameObject lucesCalido;
    [SerializeField] public GameObject postProCalido;

    [Header("Iluminacion Neutro")]
    [SerializeField] public GameObject lucesNeutro;
    [SerializeField] public GameObject postProNeutro;
    [SerializeField] public Animator animFarolesNeutro;

    [Header("Iluminacion Natural")]
    [SerializeField] public GameObject lucesNatural;
    [SerializeField] public GameObject postProNatural;
    [SerializeField] public Animator animFarolesNatural;

    public string eleccionLuces = "Ninguna";

    [Header("Instancia Descanso")]
    [SerializeField] public GameObject bandejaBebidas;

    [Header("Cuadros tirados Instancia 5")]
    [SerializeField] public GameObject cuadrosTirados;

    private static GameManager instance;

    private Camera camara;

    AudioManager audioManager;

    void Start()
    {
        eleccionActiva = false; // Inicializa el estado de la elección como no activa
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
        bandejaBebidas.SetActive(false);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
    public void SetFaseActual(int nuevaFase)
    {
        GameManager.GetInstance().faseAhora = GameManager.GetInstance().faseAhora + nuevaFase; // Actualiza la fase actual del jugador modificando la variable ubicada en GameManager
        Debug.Log("Fase actual: " + GameManager.GetInstance().faseAhora);
    }

    public void OcultarBotonesInst1()
    {
        botonesInst1.SetActive(false);
        botonesInst2.SetActive(true);
    }

    public void ApagarLuces()
    {
        audioManager.PlaySFX(audioManager.sonidoApagon);

        if (eleccionLuces == "Calido")
        {
            lucesCalido.SetActive(false);
            postProCalido.SetActive(false);
        }
        else if (eleccionLuces == "Neutro")
        {
            lucesNeutro.SetActive(false);
            postProNeutro.SetActive(false);
            animFarolesNeutro.SetBool("apagarLuces", true);
        }
        else if (eleccionLuces == "Natural")
        {
            lucesNatural.SetActive(false);
            postProNatural.SetActive(false);
            animFarolesNatural.SetBool("apagarLuces", true);
        }
        postProOscuro.SetActive(true);
    }

    public void EncenderLuces()
    {
        audioManager.PlaySFX(audioManager.sonidoPrenderLuces);

        if (eleccionLuces == "Calido")
        {
            lucesCalido.SetActive(true);
            postProCalido.SetActive(true);
        }
        else if (eleccionLuces == "Neutro")
        {
            lucesNeutro.SetActive(true);
            postProNeutro.SetActive(true);
            animFarolesNeutro.SetBool("apagarLuces", false);
        }
        else if (eleccionLuces == "Natural")
        {
            lucesNatural.SetActive(true);
            postProNatural.SetActive(true);
            animFarolesNatural.SetBool("apagarLuces", false);
        }
        postProOscuro.SetActive(false);
    }

    public void ActivarBandejaBebidas()
    {
        bandejaBebidas.SetActive(true);
    }

    public void ActivarCuadrosTirados()
    {
        cuadrosTirados.SetActive(true);
    }
}
