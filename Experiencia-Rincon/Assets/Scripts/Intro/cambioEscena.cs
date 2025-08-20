using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneChanger : MonoBehaviour
{
    public VideoPlayer reproductor; // Reproductor de video asignado en el Inspector
    public string escenaACargar; // Nombre de la escena a cargar

    private void Update()
    {
        long frame = reproductor.frame;
        long frameCount = (long)reproductor.frameCount;

        Debug.Log("Frame: " + frame + " FrameCount: " + frameCount);

        if (frame == (frameCount-1))
        {
            Debug.Log("Video terminado, cargando escena: " + escenaACargar);
            SceneManager.LoadScene(escenaACargar);
        }
    }
}