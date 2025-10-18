using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip seleccionSFX;
    public AudioClip confirmacionSFX;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
