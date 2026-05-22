using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    Coroutine playListCort;

    /* Audio Mixer */
    public AudioMixer audioMixer;

    /* Snapshots */
    [SerializeField] AudioMixerSnapshot normal, fadeOut;

    /* AudioSource */
    [SerializeField] AudioSource musicSource, uiAudioSource;

    public AudioClip countDownClip, backgroundMusic, victoryClip; // Sonido de la cuenta atrás

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /* Métodos */

    // Aplicación del nuevo volumen de la música
    public void SetMusicVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat("MusicVolume", dB);
    }

    // Aplicación del nuevo volumen de los efectos
    public void SetSFXVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat("SFXVolume", dB);
    }

    // Reproducción de algún sonido de la interfaz
    public void PlayClip(AudioClip clip)
    {
        uiAudioSource.PlayOneShot(clip);
    }

    // Transición al silencio
    public void FadeOutMusic(float time)
    {
        fadeOut.TransitionTo(time);
    }

    // Transición a la música
    public void FadeInMusic(float time)
    {
        normal.TransitionTo(time);
    }

    // Transición a la siguiente canción
    public void MakeTransition(AudioClip clip, float time)
    {
        StartCoroutine(FadeAndPlay(clip, time));
    }

    // Cambio de lista de música
    public void ChangePlaylist(List<AudioClip> numPlaylist)
    {
        if (playListCort != null)
        {
            StopCoroutine(playListCort);
        }
        playListCort = StartCoroutine(PlayPlaylist(numPlaylist));
    }

    /* Corrutinas */
    // Transición entre las canciones
    IEnumerator FadeAndPlay(AudioClip clip, float time)
    {
        FadeOutMusic(time);
        yield return new WaitForSeconds(time);
        musicSource.clip = clip;
        musicSource.Play();
        FadeInMusic(time);
    }

    // Reproducción de la lista de música
    IEnumerator PlayPlaylist(List<AudioClip> playlist)
    {
        foreach (AudioClip clip in playlist)
        {
            MakeTransition(clip, 0.5f);
            // Espera real a que termine el clip
            yield return new WaitWhile(() => musicSource.isPlaying);
        }
        StartCoroutine(PlayPlaylist(playlist)); // Bucle
    }
}