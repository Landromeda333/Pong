using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

//# Este script se encarga de gestionar el sistema de Audio #//
public class AudioManager : MonoBehaviour
{
    /* Singleton */
    public static AudioManager Instance;

    /* Audio Mixer */
    public AudioMixer audioMixer;

    /* Snapshots */
    [SerializeField] AudioMixerSnapshot normal, fadeOut;

    /* AudioSource */
    [SerializeField] AudioSource musicSource, uiAudioSource;

    public AudioClip mainMenuClip, countDownClip, backgroundMusic, victoryClip; // Sonido de la cuenta atrás, Música de fondo y música de fin de partida

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
        audioMixer.SetFloat("MusicVolume", value - 80);
    }

    // Aplicación del nuevo volumen de los efectos
    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", value - 80);
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

    // Cambio de lista de música
    public void ChangeMusic(AudioClip music)
    {
        StartCoroutine(FadeAndPlay(music, 0.3f));
    }

    public void PauseMusic(bool pause)
    {
        if (pause)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.UnPause();
        }
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
}