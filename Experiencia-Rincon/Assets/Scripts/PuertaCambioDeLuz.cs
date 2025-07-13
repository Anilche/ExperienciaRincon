using UnityEngine;

public class PuertaCambioDeLuz : MonoBehaviour
{
    public Color colorOriginal = Color.black;
    public Color colorNuevo = Color.white;
    public float duracionTransicion = 1.5f;

    private bool estaEnNuevoColor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();

            Color desde = RenderSettings.ambientLight;
            Color hacia = estaEnNuevoColor ? colorOriginal : colorNuevo;

            StartCoroutine(CambiarAmbientLight(desde, hacia, duracionTransicion));

            estaEnNuevoColor = !estaEnNuevoColor; // Alternar entre original y nuevo
        }
    }

    System.Collections.IEnumerator CambiarAmbientLight(Color inicio, Color fin, float duracion)
    {
        float t = 0;
        while (t < duracion)
        {
            t += Time.deltaTime;
            RenderSettings.ambientLight = Color.Lerp(inicio, fin, t / duracion);
            yield return null;
        }
        RenderSettings.ambientLight = fin;
    }
}
