using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancia5 : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    //private bool estaEnAreaDeElecciones = false;

    [Header("Cuadros")]
    // Pantallas que tendran animacion luego
    [SerializeField] public GameObject cuadro1;
    [SerializeField] public GameObject cuadro2;
    [SerializeField] public GameObject cuadro3;

    [Header("Indicador particulas")]
    [SerializeField] public GameObject particulas;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
