using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    private void Start()
    {
        //mixer.SetFloat("MusicVolume", PlayerPrefs.GetInt("MusicVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetInt("SFXVolume"));
        float musicVolume;
        mixer.GetFloat("MusicVolume", out musicVolume);
        Debug.Log(musicVolume);
    }
}
