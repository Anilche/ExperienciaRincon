using System.Collections;
using UnityEngine;

public class Inst2Arco : MonoBehaviour
{
    [Header("Partículas del gol")]
    [SerializeField] private ParticleSystem particulasGol;

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
        yield return new WaitForSeconds(5f);
        ps.Stop();
    }
}

