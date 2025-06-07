using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine;

public class DialogoManager : MonoBehaviour
{
    [Header("UIDialogo")]

    [SerializeField] private GameObject uiDialogo; // Referencia al UI del diálogo

    [SerializeField] private TextMeshProUGUI textoDialogo; // Referencia al componente de texto del diálogo

    private Story estaHistoria; // Referencia a la historia de Ink que se está ejecutando

    public bool dialogoActivo { get; private set; } // Indica si el diálogo está siendo reproducido o no

    private static DialogoManager instance;



    private void Awake()
    {
        // Verifica si ya existe una instancia de DialogoManager
        if (instance != null)
        {
            Debug.Log("Hay más de una instancia de DialogoManager en la escena");
        }

        instance = this;

    }

    public static DialogoManager GetInstance()
    {
        return instance;
        
    }

    private void Start()
    {
        dialogoActivo = false; // Inicializa el estado del diálogo como inactivo
        uiDialogo.SetActive(false); // Desactiva el UI del diálogo al inicio
    }

    private void Update()
    {
        if (!dialogoActivo)
        {
            // Si el diálogo no está activo, no se hace nada
            return;
        }

        
        // Si el jugador presiona la tecla E, se continúa con el diálogo
        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinuarHistoria(); // Muestra el siguiente fragmento del diálogo
        }
        
    }

    public void EntrarModoDialogo(TextAsset inkJSON)
    {
        estaHistoria = new Story(inkJSON.text); // Crea una nueva historia a partir del JSON de Ink
        dialogoActivo = true; // Cambia el estado del diálogo a activo
        uiDialogo.SetActive(true); // Activa el UI del diálogo

        ContinuarHistoria(); // Muestra el primer fragmento del diálogo o el que sigue

        Debug.Log("Entrando en modo diálogo"); // Mensaje de depuración para confirmar que se ha entrado en modo diálogo
    }

    private IEnumerator FinalizarDialogo()
    {

        yield return new WaitForSeconds(0.2f); // Espera medio segundo antes de finalizar el diálogo

        dialogoActivo = false; // Cambia el estado del diálogo a inactivo
        uiDialogo.SetActive(false); // Desactiva el UI del diálogo
        textoDialogo.text = ""; // Limpia el texto del diálogo

        Debug.Log("Finalizando diálogo"); // Mensaje de depuración para confirmar que se ha finalizado el diálogo
    }

    private void ContinuarHistoria()
    {
        if (estaHistoria.canContinue)
        {
            textoDialogo.text = estaHistoria.Continue(); // Muestra el primer fragmento del diálogo o el que sigue
            Debug.Log("Continuando historia: " + textoDialogo.text); // Mensaje de depuración para mostrar el texto actual del diálogo
        }
        else
        {
            Debug.Log("No hay más contenido en la historia"); // Mensaje de depuración si no hay más contenido
            StartCoroutine(FinalizarDialogo()); // Si no hay más contenido, finaliza el diálogo
        }
    }
}
