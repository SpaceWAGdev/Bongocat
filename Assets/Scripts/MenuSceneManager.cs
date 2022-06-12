using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void LoadMenuScene() {
        SceneManager.LoadSceneAsync(0);
    }
    public void LoadSettingsScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
