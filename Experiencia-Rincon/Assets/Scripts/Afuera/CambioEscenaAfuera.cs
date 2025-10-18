using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaAfuera : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroupIn;

    [SerializeField] private CanvasGroup canvasGroupOut;

    [SerializeField] private float fadeDuration = 5.0f;

    private void Start()
    {
        FadeIn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroupIn, canvasGroupIn.alpha, 0, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroupOut, canvasGroupOut.alpha, 0, fadeDuration));
        SceneManager.LoadScene("VideoEntrada");
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