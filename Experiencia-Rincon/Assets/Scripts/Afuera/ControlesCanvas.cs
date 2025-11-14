using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlesCanvas : MonoBehaviour
{
    public GameObject panelControles;
    public Animator animPanelControles;

    void Start()
    {
       panelControles.SetActive(false);
    }

    void Update()
    {
        StartCoroutine(Controles());
    }

    IEnumerator Controles()
    {
        yield return new WaitForSeconds(1.5f);

        panelControles.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        animPanelControles.SetBool("controlesSalida", true);
    }
}
