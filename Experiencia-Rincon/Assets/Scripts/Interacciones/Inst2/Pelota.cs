using UnityEngine;

public class PelotaSonido : MonoBehaviour
{
    AudioManager audioManager;

    [Header("Cooldown del sonido")]
    [SerializeField] private float cooldownSonido = 0.4f;

    private float ultimoGolpe = -10f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio")
                                 .GetComponent<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (Time.time - ultimoGolpe < cooldownSonido)
            return;

        ultimoGolpe = Time.time;
        audioManager.PlaySFX(audioManager.sonidoPatadaPelota);
    }
}
