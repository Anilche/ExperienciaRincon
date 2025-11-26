using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFinal : MonoBehaviour
{
    [Header("Portal")]
    [SerializeField] public GameObject portal;
    [SerializeField] public GameObject puertaTrigger;

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup canvasGroupOut;
    [SerializeField] private float fadeDuration = 5.0f;

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
            FadeOut();
        }
    }

    public void FadeOut()
    {
        //StartCoroutine(FadeCanvasGroup(canvasGroupOut, canvasGroupOut.alpha, 0, fadeDuration));
        SceneManager.LoadScene("RinconBonus");
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}
