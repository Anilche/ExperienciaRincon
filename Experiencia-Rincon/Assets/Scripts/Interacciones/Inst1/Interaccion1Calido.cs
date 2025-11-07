using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interaccion1Calido : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool estaEnAreaDeInteraccion = false;

    private string tagSeleccionable = "Seleccionable";

    private int contadorRamitasRecogidas = 0;

    [Header("Ramitas")]
    [SerializeField] public GameObject ramita1;
    [SerializeField] public GameObject ramita2;
    [SerializeField] public GameObject ramita3;
    [SerializeField] public GameObject ramita4;
    [SerializeField] public GameObject ramita5;
    [SerializeField] public GameObject ramita6;
    [SerializeField] public GameObject ramita7;
    [SerializeField] public GameObject ramita8;
    [SerializeField] public GameObject ramita9;
    [SerializeField] public GameObject ramita10;
    [SerializeField] public GameObject ramita11;
    [SerializeField] public GameObject ramita12;

    [Header("Hojitas")]
    [SerializeField] public GameObject hojita1;
    [SerializeField] public GameObject hojita2;
    [SerializeField] public GameObject hojita3;
    [SerializeField] public GameObject hojita4;
    [SerializeField] public GameObject hojita5;
    [SerializeField] public GameObject hojita6;
    [SerializeField] public GameObject hojita7;
    [SerializeField] public GameObject hojita8;
    [SerializeField] public GameObject hojita9;
    [SerializeField] public GameObject hojita10;

    [Header("Montones Ramitas Carretilla")]
    [SerializeField] public GameObject montonRamitas1;
    [SerializeField] public GameObject montonRamitas2;
    [SerializeField] public GameObject montonRamitas3;

    [Header("Distancia máxima de interacción")]
    [SerializeField] private float distanciaMaxima = 3f; // límite de alcance

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }

    void Update()
    {

        if (GameManager.GetInstance().faseAhora >= numFaseNecesaria && estaEnAreaDeInteraccion)
        {
            ramita1.tag = tagSeleccionable;
            ramita2.tag = tagSeleccionable;
            ramita3.tag = tagSeleccionable;
            ramita4.tag = tagSeleccionable;
            ramita5.tag = tagSeleccionable;
            ramita6.tag = tagSeleccionable;
            ramita7.tag = tagSeleccionable;
            ramita8.tag = tagSeleccionable;
            ramita9.tag = tagSeleccionable;
            ramita10.tag = tagSeleccionable;
            ramita11.tag = tagSeleccionable;
            ramita12.tag = tagSeleccionable;
            hojita1.tag = tagSeleccionable;
            hojita2.tag = tagSeleccionable;
            hojita3.tag = tagSeleccionable;
            hojita4.tag = tagSeleccionable;
            hojita5.tag = tagSeleccionable;
            hojita6.tag = tagSeleccionable;
            hojita7.tag = tagSeleccionable;
            hojita8.tag = tagSeleccionable;
            hojita9.tag = tagSeleccionable;
            hojita10.tag = tagSeleccionable;

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

                    Debug.Log(highlight.gameObject);

                    string objetoSeleccionado = highlight.gameObject.name;

                    switch (objetoSeleccionado)
                    {
                        case "Ramitas1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita1.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita2.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita3.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas4":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita4.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas5":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita5.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas6":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita6.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas7":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita7.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas8":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita8.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas9":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita9.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas10":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita10.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas11":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita11.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Ramitas12":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            ramita12.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas1":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita1.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas2":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita2.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas3":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita3.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas4":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita4.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas5":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita5.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas6":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita6.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas7":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita7.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas8":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita8.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas9":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita9.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
                            break;

                        case "Hojas10":
                            selection.gameObject.GetComponent<Outline>().enabled = false;

                            audioManager.PlaySFX(audioManager.seleccionSFX);

                            hojita10.SetActive(false);
                            contadorRamitasRecogidas++;
                            Debug.Log("Hojas y Ramitas recogidas: " + contadorRamitasRecogidas);
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

        if (contadorRamitasRecogidas == 1)
        {
            montonRamitas1.SetActive(true);
        }
        if (contadorRamitasRecogidas == 8)
        {
            montonRamitas2.SetActive(true);
        }
        if (contadorRamitasRecogidas == 15)
        {
            montonRamitas3.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            estaEnAreaDeInteraccion = true;
            Debug.Log("Jugador en area de interaccion calido");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GetInstance().faseAhora >= numFaseNecesaria)
        {
            estaEnAreaDeInteraccion = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaEnAreaDeInteraccion = false;
            Debug.Log("Jugador salio del area de interaccion calido");
        }
    }
}
