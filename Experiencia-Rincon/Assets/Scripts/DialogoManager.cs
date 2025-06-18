using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine;

public class DialogoManager : MonoBehaviour
{
    [Header("UIDialogo")]

    [SerializeField] private GameObject uiDialogo; // Referencia al UI del di�logo

    [SerializeField] private TextMeshProUGUI textoDialogo; // Referencia al componente de texto del di�logo

    private Story estaHistoria; // Referencia a la historia de Ink que se est� ejecutando

    public bool dialogoActivo { get; private set; } // Indica si el di�logo est� siendo reproducido o no

    private static DialogoManager instance;

    private bool puedeAvanzar = false; // Indica si el jugador puede avanzar en el di�logo

    private void Awake()
    {
        // Verifica si ya existe una instancia de DialogoManager
        if (instance != null)
        {
            Debug.Log("Hay m�s de una instancia de DialogoManager en la escena");
        }

        instance = this;

    }

    public static DialogoManager GetInstance()
    {
        return instance;
        
    }

    private void Start()
    {
        dialogoActivo = false; // Inicializa el estado del di�logo como inactivo
        uiDialogo.SetActive(false); // Desactiva el UI del di�logo al inicio
    }

    private void Update()
    {
        if (!dialogoActivo || !puedeAvanzar)
        {
            // Si el di�logo no est� activo, no se hace nada
            return;
        }

        
        // Si el jugador presiona la tecla E, se contin�a con el di�logo
        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinuarHistoria(); // Muestra el siguiente fragmento del di�logo
        }
        
    }

    private void RegistrarFuncionesExternas()
    {
        estaHistoria.BindExternalFunction("SetFaseActual", (int nuevaFase) =>
        {
            GameManager.GetInstance().SetFaseActual(nuevaFase);
        });
    }

    public void EntrarModoDialogo(TextAsset inkJSON)
    {
        estaHistoria = new Story(inkJSON.text); // Crea una nueva historia a partir del JSON de Ink

        
        RegistrarFuncionesExternas(); // Registra las funciones externas que se pueden llamar desde Ink
        

        dialogoActivo = true; // Cambia el estado del di�logo a activo
        uiDialogo.SetActive(true); // Activa el UI del di�logo

        ContinuarHistoria(); // Muestra el primer fragmento del di�logo o el que sigue

        puedeAvanzar = false;
        StartCoroutine(EsperarAntesDePermitirAvance()); // Inicia una corrutina para esperar antes de permitir que el jugador avance en el di�logo

        Debug.Log("Entrando en modo di�logo");
    }

    private IEnumerator FinalizarDialogo()
    {

        yield return new WaitForSeconds(0.2f); // Espera medio segundo antes de finalizar el di�logo

        dialogoActivo = false; // Cambia el estado del di�logo a inactivo
        uiDialogo.SetActive(false); // Desactiva el UI del di�logo
        textoDialogo.text = ""; // Limpia el texto del di�logo

        Debug.Log("Finalizando di�logo");
    }

    private void ContinuarHistoria()
    {
        if (estaHistoria.canContinue)
        {
            textoDialogo.text = estaHistoria.Continue(); // Muestra el primer fragmento del di�logo o el que sigue
            Debug.Log("Continuando historia: " + textoDialogo.text);
        }
        else
        {
            StartCoroutine(FinalizarDialogo()); // Si no hay m�s contenido, finaliza el di�logo
        }
    }

    private IEnumerator EsperarAntesDePermitirAvance()
    {
        yield return new WaitForSeconds(0.2f); // Espera medio segundo antes de permitir que el jugador avance en el di�logo
        puedeAvanzar = true; // Permite que el jugador avance en el di�logo
    }

}
