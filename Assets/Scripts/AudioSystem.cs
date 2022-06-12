using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioSource destroySound;
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource successSound;
    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetInt("SFXVolume"));
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetInt("MusicVolume"));
    }

    public void PlayDamageSound()
    {
        damageSound.Play();
    }

    public void PlayDestroySound()
    {
        destroySound.Play();
    }

    public void PlayGameOverSound()
    {
        gameOverSound.Play();
    }

    public void PlaySuccessSound()
    {
        successSound.Play();
    }
}
