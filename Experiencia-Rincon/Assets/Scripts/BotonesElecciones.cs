using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotonesElecciones : MonoBehaviour
{
    // Se declaran los objetos que luego referenciar�n al script Elecciones
    private GameObject objeto1;
    private GameObject objeto2;
    private GameObject objeto3;

    /*
    private GameObject animObjeto1;
    private GameObject animObjeto2;
    private GameObject animObjeto3;*/

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoSeleccion;


    // Referencia al script Elecciones para que sea mas f�cil de llamar
    private Elecciones elecciones;

    private void Start()
    {
        // Se obtiene la instancia del script Elecciones y se asignan los objetos a las variables ya creadas, para manejarlo m�s f�cilmente
        elecciones = Elecciones.GetInstance();
        objeto1 = elecciones.objeto1;
        objeto2 = elecciones.objeto2;
        objeto3 = elecciones.objeto3;

        /* En caso de tener mas de una animaci�n por objeto
        animObjeto1 = elecciones.animObjeto1;
        animObjeto2 = elecciones.animObjeto2;
        animObjeto3 = elecciones.animObjeto3;*/
    }

    public void Eleccion1()
    {
        // Se activa el objeto 1 y se desactivan los dem�s
        objeto1.SetActive(true);
        objeto2.SetActive(false);
        objeto3.SetActive(false);

        //animObjeto1.GetComponent<Animator>().Play("prueba");

        audioSource.PlayOneShot(sonidoSeleccion);
    }

    public void Eleccion2()
    {
        // Se activa el objeto 2 y se desactivan los dem�s
        objeto1.SetActive(false);
        objeto2.SetActive(true);
        objeto3.SetActive(false);

        audioSource.PlayOneShot(sonidoSeleccion);
    }

    public void Eleccion3()
    {
        // Se activa el objeto 3 y se desactivan los dem�s
        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(true);

        audioSource.PlayOneShot(sonidoSeleccion);
    }

    public void Confirmar()
    {
        // Se confirma la selecci�n y se cierra el UI de elecciones
        audioSource.PlayOneShot(sonidoSeleccion);
        Elecciones.GetInstance().ConfirmarSeleccion();

        /*
        if (GameManager.GetInstance().faseAhora > Elecciones.GetInstance().numFaseNecesaria)
        {
            // Si la fase actual es mayor a la necesaria, no se hace nada
            return;
        }
        else if (GameManager.GetInstance().faseAhora == Elecciones.GetInstance().numFaseNecesaria)
        {
            // Si la fase actual es igual a la necesaria, se incrementa la fase actual
            GameManager.GetInstance().SetFaseActual(1);
            Debug.Log("Fase actual incrementada a: " + GameManager.GetInstance().faseAhora);
        }*/
    }
}
