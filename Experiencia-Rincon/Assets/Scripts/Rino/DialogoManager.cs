using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DialogoManager : MonoBehaviour
{
    [Header("UIDialogo")]

    [SerializeField] private GameObject uiDialogo; // Referencia al UI del diálogo

    [SerializeField] private TextMeshProUGUI textoDialogo; // Referencia al componente de texto del diálogo

    private Story estaHistoria; // Referencia a la historia de Ink que se está ejecutando

    public bool dialogoActivo { get; private set; } // Indica si el diálogo está siendo reproducido o no

    private static DialogoManager instance;

    private bool puedeAvanzar = false; // Indica si el jugador puede avanzar en el diálogo

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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
        if (!dialogoActivo || !puedeAvanzar)
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

    private void RegistrarFuncionesExternas()
    {
        estaHistoria.BindExternalFunction("SetFaseActual", (int nuevaFase) =>
        {
            GameManager.GetInstance().SetFaseActual(nuevaFase);
        });

        estaHistoria.BindExternalFunction("GetFase", () =>
        {
            return GameManager.GetInstance().faseAhora;
        });

        estaHistoria.BindExternalFunction("ReproducirDialogo", (int numeroLineaDeVoz) =>
        {
            audioManager.PlayDialogo(numeroLineaDeVoz);
        });

        estaHistoria.BindExternalFunction("OcultarBotonesInst1", () =>
        {
            GameManager.GetInstance().OcultarBotonesInst1();
        });

        estaHistoria.BindExternalFunction("ApagarLuces", () =>
        {
            GameManager.GetInstance().ApagarLuces();
        });

        estaHistoria.BindExternalFunction("ActivarBandejaBebidas", () =>
        {
            GameManager.GetInstance().ActivarBandejaBebidas();
        });

        estaHistoria.BindExternalFunction("PasarEscenaA", (string escena) =>
        {
            SceneManager.LoadScene(escena);
        });

        estaHistoria.BindExternalFunction("ActivarCuadrosTirados", () =>
        {
            GameManager.GetInstance().ActivarCuadrosTirados();
        });
    }

    private void DejarDeRegistrarFuncionesExternas()
    {
        estaHistoria.UnbindExternalFunction("SetFaseActual");
        estaHistoria.UnbindExternalFunction("GetFase");
    }

    public void EntrarModoDialogo(TextAsset inkJSON)
    {
        estaHistoria = new Story(inkJSON.text); // Crea una nueva historia a partir del JSON de Ink

        RegistrarFuncionesExternas(); // Registra las funciones externas que se pueden llamar desde Ink
        
        dialogoActivo = true; // Cambia el estado del diálogo a activo
        uiDialogo.SetActive(true); // Activa el UI del diálogo

        ContinuarHistoria(); // Muestra el primer fragmento del diálogo o el que sigue

        puedeAvanzar = false;
        StartCoroutine(EsperarAntesDePermitirAvance()); // Inicia una corrutina para esperar antes de permitir que el jugador avance en el diálogo

        //Debug.Log("Entrando en modo diálogo");
    }

    private IEnumerator FinalizarDialogo()
    {

        yield return new WaitForSeconds(0.2f); // Espera medio segundo antes de finalizar el diálogo

        dialogoActivo = false; // Cambia el estado del diálogo a inactivo
        uiDialogo.SetActive(false); // Desactiva el UI del diálogo
        textoDialogo.text = ""; // Limpia el texto del diálogo

        //Debug.Log("Finalizando diálogo");

        //DejarDeRegistrarFuncionesExternas(); // Deja de registrar las funciones externas

        MovimientoRino.GetInstance().moverRino();
    }

    private void ContinuarHistoria()
    {
        // Verifica si la historia puede continuar
        while (estaHistoria.canContinue)
        {
            string linea = estaHistoria.Continue();

            // Si la línea no está vacía o no son solo espacios
            if (!string.IsNullOrWhiteSpace(linea))
            {
                textoDialogo.text = linea;
                Debug.Log("Continuando historia: " + linea);
                return; // Salimos de la función mostrando esta línea
            }

            // Si está vacía, seguimos al siguiente fragmento automáticamente
            //Debug.Log("Línea vacía ignorada");
        }
        
        // Si no hay más contenido después de ignorar líneas vacías
        //Debug.Log("No hay más contenido en la historia");
        StartCoroutine(FinalizarDialogo());
    }

    private IEnumerator EsperarAntesDePermitirAvance()
    {
        yield return new WaitForSeconds(0.2f); // Espera medio segundo antes de permitir que el jugador avance en el diálogo
        puedeAvanzar = true; // Permite que el jugador avance en el diálogo
    }

}
