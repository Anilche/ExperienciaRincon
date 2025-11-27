using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInstancia3 : MonoBehaviour
{
    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Objetos a Mostrarse")]
    [SerializeField] public GameObject Objetos;
    [SerializeField] public GameObject estanteriaSeleccion;
    [SerializeField] public GameObject estanteriaFinal;


    private void Awake()
    {
        Objetos.SetActive(false); // Desactiva los objetos al inicio
        estanteriaSeleccion.SetActive(false); // Desactiva la estanteria al inicio
        estanteriaFinal.SetActive(false); // Activa la estanteria final al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            Objetos.SetActive(true);
            estanteriaSeleccion.SetActive(true);
            estanteriaFinal.SetActive(true);
            Debug.Log("Spawneando objetos");
            Destroy(this.gameObject);
        }
    }
    
}
