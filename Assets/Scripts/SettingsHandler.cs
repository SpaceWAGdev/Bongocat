using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    int SFXVolume;
    int MusicVolume;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXVolume = PlayerPrefs.GetInt("SFXVolume");
        }
        else
        {
            PlayerPrefs.SetInt("SFXVolume", -10);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            MusicVolume = PlayerPrefs.GetInt("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetInt("MusicVolume", -10);
        }
        PlayerPrefs.Save();
    }
    public void UpdateSFXVolume(int volume)
    {
        mixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetInt("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", GameObject.FindGameObjectWithTag("SFXVolume").GetComponent<Slider>().value);
        PlayerPrefs.SetInt("SFXVolume", (int)GameObject.FindGameObjectWithTag("SFXVolume").GetComponent<Slider>().value);
        PlayerPrefs.Save();
    }

    public void UpdateMusicVolume(int volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetInt("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void UpdateMusicVolume()
    {
        mixer.SetFloat("MusicVolume", GameObject.FindGameObjectWithTag("MusicVolume").GetComponent<Slider>().value);
        PlayerPrefs.SetInt("MusicVolume", (int)GameObject.FindGameObjectWithTag("MusicVolume").GetComponent<Slider>().value);
        PlayerPrefs.Save();
    }

    public void OpenNewAnonIssue(){
        Application.OpenURL("https://forms.gle/Gb7PxMNAXsAdPye3A");
    }

    public void OpenGithubIssue(){
        Application.OpenURL("https://github.com/SpaceWAGdev/Bongocat/issues/new");
    }
}
