using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFinal : MonoBehaviour
{
    [Header("Portal")]
    [SerializeField] public GameObject portal;
    [SerializeField] public GameObject puertaTrigger;

    private bool puedeEntrar = false;

    [Header("Requerimientos para utilizarse")]
    [SerializeField] public int numFaseNecesaria; // Requerimiento para poder activar el trigger de elecciones

    void Update()
    {
        if (GameManager.GetInstance().faseAhora == numFaseNecesaria)
        {
            puedeEntrar = true;
            portal.SetActive(true);
        }
    }

    private void Start()
    {
        portal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && puedeEntrar == true)
        {
            SceneManager.LoadScene("RinconBonus");
        }
    }
}
