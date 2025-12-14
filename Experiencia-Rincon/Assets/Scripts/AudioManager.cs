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
    public AudioClip sonidoApagon;
    public AudioClip sonidoPrenderLuces;
    public AudioClip sonidoCortina;
    public AudioClip sonidoGol;
    public AudioClip sonidoVentana;
    public AudioClip sonidoGuitarra;
    public AudioClip sonidoPiano;
    public AudioClip sonidoMixer;
    public AudioClip sonidoMicrofono;
    public AudioClip sonidoMate;
    public AudioClip sonidoTeCafe;
    public AudioClip sonidoPedido;
    public AudioClip sonidoTV;
    public AudioClip sonidoMoto;
    public AudioClip sonidoLucesCuadro;
    public AudioClip sonidoPato;
    public AudioClip sonidoCascabel;

    [Header("Audio Clips Dialogos")]
    public AudioClip f0;
    public AudioClip f1;
    public AudioClip f2;
    public AudioClip f3;
    public AudioClip f4;
    public AudioClip f5;
    public AudioClip f6;
    public AudioClip f7;
    public AudioClip f8;
    public AudioClip f9;
    public AudioClip f10;
    public AudioClip f11;
    public AudioClip f12;
    public AudioClip f13;
    public AudioClip f14;
    public AudioClip f15;
    public AudioClip f16;
    public AudioClip f17;
    public AudioClip f18;
    public AudioClip f19;
    public AudioClip f20;
    public AudioClip f21;
    public AudioClip f22;
    public AudioClip f23;
    public AudioClip f24;
    public AudioClip f25;
    public AudioClip f26;
    public AudioClip f27;
    public AudioClip f28;
    public AudioClip f29;
    public AudioClip f30;
    public AudioClip f31;
    public AudioClip f32;
    public AudioClip f33;
    public AudioClip f34;
    public AudioClip f35;
    public AudioClip f36;
    public AudioClip f37;
    public AudioClip f38;
    public AudioClip f39;
    public AudioClip f40;
    public AudioClip f41;

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
                dialogosSource.PlayOneShot(f1);
                break;

            case 2:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f2);
                break;

            case 3:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f3);
                break;

            case 4:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f4);
                break;

            case 5:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f5);
                break;

            case 6:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f6);
                break;

            case 7:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f7);
                break;

            case 8:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f8);
                break;

            case 9:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f9);
                break;

            case 10:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f10);
                break;

            case 11:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f11);
                break;

            case 12:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f12);
                break;

            case 13:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f13);
                break;

            case 14:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f14);
                break;

            case 15:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f15);
                break;

            case 16:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f16);
                break;

            case 17:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f17);
                break;

            case 18:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f18);
                break;

            case 19:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f19);
                break;

            case 20:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f20);
                break;

            case 21:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f21);
                break;

            case 22:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f22);
                break;

            case 23:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f23);
                break;

            case 24:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f24);
                break;

            case 25:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f25);
                break;

            case 26:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f26);
                break;

            case 27:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f27);
                break;

            case 28:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f28);
                break;

            case 29:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f29);
                break;

            case 30:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f30);
                break;

            case 31:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f31);
                break;

            case 32:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f32);
                break;

            case 33:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f33);
                break;

            case 34:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f34);
                break;

            case 35:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f35);
                break;

            case 36:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f36);
                break;

            case 37:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f37);
                break;

            case 38:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f38);
                break;
/*
            case 39:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f39);
                break;

            case 40:
                dialogosSource.Stop();
                dialogosSource.PlayOneShot(f40);
                break;*/
        }
    }
}
