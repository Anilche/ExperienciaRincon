using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AparecerBotonesFin : MonoBehaviour
{
    public VideoPlayer reproductor; // Reproductor de video asignado en el Inspector

    [Header("Elementos a mostrar al final del video")]
    [SerializeField] public GameObject botonesFinal; // Botones a mostrar al final del video

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        botonesFinal.SetActive(false);
    }

    private void Update()
    {
        long frame = reproductor.frame;
        long frameCount = (long)reproductor.frameCount;

        Debug.Log("Frame: " + frame + " FrameCount: " + frameCount);

        if (frame == (frameCount-1))
        {
            Debug.Log("Video terminado, prendiendo botones");
            botonesFinal.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
