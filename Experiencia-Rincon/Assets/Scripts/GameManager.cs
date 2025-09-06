using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int faseAhora; // Fase actual del jugador, se puede setear desde otro script

    public bool eleccionActiva;

    private static GameManager instance;

    [SerializeField] private CinemachineVirtualCamera vcamJugador;
    [SerializeField] private CinemachineVirtualCamera vcamHabilitarBonus;

    private void Update()
    {
        /* final
        if (faseAhora == 5)
        {
            StartCoroutine(DesbloqueoSalaBonus()); // Llama a la función para desbloquear la sala bonus si la fase actual es X
        }*/
    }

    void Start()
    {
        faseAhora = 0; // Inicializa la fase actual del jugador
        eleccionActiva = false; // Inicializa el estado de la elección como no activa
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        instance = this;
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
    }
}
