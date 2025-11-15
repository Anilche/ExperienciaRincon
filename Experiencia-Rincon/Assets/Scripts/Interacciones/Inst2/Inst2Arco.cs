using System.Collections;
using UnityEngine;

public class Inst2Arco : MonoBehaviour
{
    [Header("Partículas del gol")]
    [SerializeField] private ParticleSystem particulasGol;

    private Camera camara;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camara = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pelota"))
        {
            StartCoroutine(prenderParticulas(particulasGol));
        }
    }
    IEnumerator prenderParticulas(ParticleSystem ps)
    {
        ps.Play();
        audioManager.PlaySFX(audioManager.sonidoGol);
        yield return new WaitForSeconds(5f);
        ps.Stop();
    }
}

