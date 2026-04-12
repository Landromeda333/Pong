using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    /* Audio Mixer */
    public AudioMixer audioMixer;

    [SerializeField] AudioMixerSnapshot normal, fadeOut;

    [SerializeField] AudioSource musicSource, uiAudioSource;

    public AudioClip coundDownClip, backgroundMusic, victoryClip; // Sonido de la cuenta atrás

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            uiAudioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    // Aplicación del nuevo volumen de la música
    public void SetMusicVolume(float value)
    {
        float dB = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MusicVolume", dB);
    }

    // Aplicación del nuevo volumen de los efectos
    public void SetSFXVolume(float value)
    {
        float dB = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("SFXVolume", dB);
    }

    // Reproducción de algún sonido de la interfaz
    public void PlayClip(AudioClip clip)
    {
        uiAudioSource.PlayOneShot(clip);
    }

    public void FadeOutMusic(float time)
    {
        fadeOut.TransitionTo(time);
    }

    public void FadeInMusic(float time)
    {
        normal.TransitionTo(time);
    }

    public void MakeTransition(AudioClip clip, float startTime)
    {
        StartCoroutine(FadeAndPlay(clip, startTime,1));
    }

    IEnumerator FadeAndPlay(AudioClip clip, float startTime, float time)
    {
        yield return new WaitForSeconds(time);
        FadeOutMusic(time);
        yield return new WaitForSeconds(time);
        musicSource.clip = clip;
        musicSource.Play();
        FadeInMusic(time);
    }
}