using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int faseAhora = 0; // Fase actual del jugador, se puede setear desde otro script

    public bool eleccionActiva;

    [SerializeField] public GameObject botonesInst1;
    [SerializeField] public GameObject botonesInst2;

    [Header("Iluminacion")]
    [SerializeField] public GameObject lucesCalido;
    [SerializeField] public GameObject postProCalido;

    [SerializeField] public GameObject lucesNeutro;
    [SerializeField] public GameObject postProNeutro;

    [SerializeField] public GameObject lucesNatural;
    [SerializeField] public GameObject postProNatural;

    [SerializeField] public GameObject postProOscuro;

    public string eleccionLuces = "Ninguna";

    [Header("Instancia Descanso")]
    [SerializeField] public GameObject bandejaBebidas;

    [Header("Cuadros tirados Instancia 5")]
    [SerializeField] public GameObject cuadrosTirados;

    private static GameManager instance;

    //[Header("Camara jugador")]
    //[SerializeField] private CinemachineVirtualCamera vcamJugador;
    //[SerializeField] private CinemachineVirtualCamera vcamHabilitarBonus;

    private Camera camara;

    AudioManager audioManager;

    private void Update()
    {
        /* final
        if (faseAhora == 5)
        {
            StartCoroutine(DesbloqueoSalaBonus()); // Llama a la función para desbloquear la sala bonus si la fase actual es X
        }*/

        if (Input.GetKeyDown(KeyCode.O))
        {
            SetFaseActual(1);
        }
    }

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
        }
        else if (eleccionLuces == "Natural")
        {
            lucesNatural.SetActive(false);
            postProNatural.SetActive(false);
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
        }
        else if (eleccionLuces == "Natural")
        {
            lucesNatural.SetActive(true);
            postProNatural.SetActive(true);
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


    /*
    private IEnumerator DesbloqueoSalaBonus()
    {
        SetFaseActual(1); // Aumenta la fase actual en 1 al desbloquear la sala bonus (para que deje de llamarse la función en cada frame)

        ActivarCamaraDesbloqueo(); // Cambia a la cámara de desbloqueo

        ///////////////////// Acá va la animación de desbloqueo de la sala bonus

        Debug.Log("Sala bonus desbloqueada");

        yield return new WaitForSeconds(5f);

        ActivarCamaraJugador(); // Vuelve a activar la cámara del jugador después de la animación de desbloqueo

    }
    
    private void ActivarCamaraJugador()
    {
        vcamJugador.Priority = 10;
        vcamHabilitarBonus.Priority = 0;
    }

    private void ActivarCamaraDesbloqueo()
    {
        vcamJugador.Priority = 0;
        vcamHabilitarBonus.Priority = 10;
    }*/
}
