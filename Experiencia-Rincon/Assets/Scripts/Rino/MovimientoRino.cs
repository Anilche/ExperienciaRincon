using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRino : MonoBehaviour
{
    private static MovimientoRino instance;

    public List<Transform> puntosDeMovimiento=new List<Transform>(); // Lista de puntos a los que el Rino se moverá
    public bool enMovimiento; // Indica si el Rino está en movimiento
    public int indicePuntoActual = 0; // Índice del punto actual al que se moverá el Rino
    public float velocidad; // Velocidad a la que se moverá el Rino

    private void Update()
    {
        if (enMovimiento)
        {
            // Mueve el Rino hacia el punto actual en la lista de puntos de movimiento
            transform.position = Vector3.MoveTowards(transform.position, puntosDeMovimiento[indicePuntoActual].position, velocidad * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, puntosDeMovimiento[indicePuntoActual].position) < 0.01f)
        {
            enMovimiento = false;
        }
    }

    private void Awake()
    {
        instance = this;
        enMovimiento = false; // Inicialmente, el Rino no está en movimiento
    }
    public static MovimientoRino GetInstance()
    {
        return instance;
    }

    public void moverRino()
    {
        chequearIndice(); // Verifica y cambia el punto al cual moverse según la fase del juego
        enMovimiento = true; // Cambia el estado de movimiento a verdadero
    }

    void chequearIndice ()
    {
        switch (GameManager.GetInstance().faseAhora)
        {
            case 1:
                indicePuntoActual = 0; // Actualiza el índice del punto actual
                break;

            case 8:
                indicePuntoActual = 1;
                break;

            case 11:
                indicePuntoActual = 2;
                break;

            case 16:
                indicePuntoActual = 3;
                break;

            default:
                // Lógica para otros puntos
                break;
        }
    }
}
