using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotonesElecciones : MonoBehaviour
{
    // Se declaran los objetos que luego referenciarán al script Elecciones
    private GameObject objeto1;
    private GameObject objeto2;
    private GameObject objeto3;

    // Referencia al script Elecciones para que sea mas fácil de llamar
    private Elecciones elecciones;

    private void Start()
    {
        // Se obtiene la instancia del script Elecciones y se asignan los objetos a las variables ya creadas, para manejarlo más fácilmente
        elecciones = Elecciones.GetInstance();
        objeto1 = elecciones.objeto1;
        objeto2 = elecciones.objeto2;
        objeto3 = elecciones.objeto3;
    }

    public void Eleccion1()
    {
        // Se activa el objeto 1 y se desactivan los demás
        objeto1.SetActive(true);
        objeto2.SetActive(false);
        objeto3.SetActive(false);
    }

    public void Eleccion2()
    {
        // Se activa el objeto 2 y se desactivan los demás
        objeto1.SetActive(false);
        objeto2.SetActive(true);
        objeto3.SetActive(false);
    }

    public void Eleccion3()
    {
        // Se activa el objeto 3 y se desactivan los demás
        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(true);
    }

    public void Confirmar()
    {
        // Se confirma la selección y se cierra el UI de elecciones
        Elecciones.GetInstance().ConfirmarSeleccion();
    }
}
