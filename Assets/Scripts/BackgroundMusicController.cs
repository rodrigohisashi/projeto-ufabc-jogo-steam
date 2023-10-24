using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance = null;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        // Verifique se a cena atual é a cena de tutorial e pause a música de fundo se necessário
    // Verifique se a cena atual é a cena de tutorial ou se há outros sons sendo reproduzidos
        if (SceneManager.GetActiveScene().name == "Tutorial" || OtherAudioPlaying())
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        // Se não estiver na cena de tutorial e a música de fundo estiver em pausa, retome a reprodução
        else
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }

     private bool OtherAudioPlaying()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource && source.isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}
