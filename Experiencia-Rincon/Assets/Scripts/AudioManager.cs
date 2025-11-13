using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource dialogosSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip seleccionSFX;
    public AudioClip confirmacionSFX;
    public AudioClip sonidoMoneda;
    public AudioClip sonidoMordida;
    public AudioClip sonidoCampanaComida;

    [Header("Audio Clips Dialogos")]
    public AudioClip f0;
    public AudioClip f02;
    public AudioClip f1;
    public AudioClip f2;
    public AudioClip f22;
    public AudioClip f3;
    public AudioClip f4;
    public AudioClip f5;
    public AudioClip f6;
    public AudioClip f7;
    public AudioClip f8;
    public AudioClip f9;
    public AudioClip f10;
    public AudioClip f102;
    public AudioClip f11;
    public AudioClip f112;
    public AudioClip f12;
    public AudioClip f13;
    public AudioClip f132;
    public AudioClip f14;
    public AudioClip f15;
    public AudioClip f16;
    public AudioClip f162;
    public AudioClip f163;
    public AudioClip f164;
    public AudioClip f165;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.Stop();
        Debug.Log("Changing music to: " + clip.name);
        musicSource.clip = clip;
        musicSource.volume = 0.05f;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDialogo(int numClip)
    {
        switch (numClip) {
            case 0:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f0);
                break;

            case 1:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f02);
                break;

            case 2:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f1);
                break;

            case 3:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f2);
                break;

            case 4:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f22);
                break;

            case 5:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f3);
                break;

            case 6:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f4);
                break;

            case 7:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f5);
                break;

            case 8:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f6);
                break;

            case 9:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f7);
                break;

            case 10:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f8);
                break;

            case 11:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f9);
                break;

            case 12:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f10);
                break;

            case 13:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f102);
                break;

            case 14:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f11);
                break;

            case 15:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f112);
                break;

            case 16:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f12);
                break;

            case 17:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f13);
                break;

            case 18:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f132);
                break;

            case 19:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f14);
                break;

            case 20:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f15);
                break;

            case 21:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f16);
                break;

            case 22:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f162);
                break;

            case 23:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f163);
                break;

            case 24:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f164);
                break;

            case 25:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f165);
                break;
        }
    }
}
