using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TriggerVideoRincon : MonoBehaviour
{

    public VideoPlayer videoPlayer;   // referencia al VideoPlayer
    public RawImage videoUI;          // la RawImage del canvas
    private bool videoPlaying = false;

    private void Start()
    {
        // Asegurar que la UI esté desactivada al inicio
        videoUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !videoPlaying)
        {
            videoPlaying = true;
            StartCoroutine(PlayCutscene());
            Debug.Log("Video started");
        }
    }

    private IEnumerator PlayCutscene()
    {
        // Activar RawImage
        videoUI.gameObject.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;

        // Reproducir video
        videoPlayer.Play();

        // Esperar a que termine
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Ocultar RawImage
        videoUI.gameObject.SetActive(false);

        // Reanudar el juego
        Time.timeScale = 1f;
    }


    /*
    public VideoPlayer videoPlayer; // Reproductor de video asignado en el Inspector
    private bool videoPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !videoPlaying)
        {
            videoPlaying = true;
            StartCoroutine(PlayCutscene());
            Debug.Log("Video started");
        }
    }
    
    private System.Collections.IEnumerator PlayCutscene()
    {
        // Pausar el juego
        Time.timeScale = 0f;

        // Reproducir el video
        videoPlayer.Play();

        // Esperar a que termine
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Reanudar el juego
        Time.timeScale = 1f;

    }

    */

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ReproducirVideo());
            //Corrutina?
            //Pausar Fondo
            
            //Reproducir video Rincon
            //Despausar fondo
            
            //Destruir este objeto
        }
    }

    private IEnumerator ReproducirVideo()
    {
        Time.timeScale = 0;

        long frame = reproductor.frame;
        long frameCount = (long)reproductor.frameCount;

        reproductor.Play();

        Debug.Log("Frame: " + frame + " FrameCount: " + frameCount);

        if (frame == (frameCount - 1))
        {
            Debug.Log("Video terminado");
            Time.timeScale = 1;

        }
        yield return null;
    }*/
}
