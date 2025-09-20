using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInstancia3 : MonoBehaviour
{

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    [Header("Objetos a Mostrarse")]
    [SerializeField] public GameObject Objeto1;
    //[SerializeField] public GameObject Objeto2;

    //Ver tema animaciones

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            Objeto1.SetActive(true);
            //Objeto2.SetActive(true);
            Debug.Log("Spawneando objetos");
        }
    }
    
}
