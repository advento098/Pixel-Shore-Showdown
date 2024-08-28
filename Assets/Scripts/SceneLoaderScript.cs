using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoaderScript : MonoBehaviour
{
    public void LoadPlayScene(string sceneName)
    {
        FindObjectOfType<AudioManager>().Play("Button Click");
        FindObjectOfType<AudioManager>().Stop("Background");
        FindObjectOfType<AudioManager>().Play("Background 2");
        SceneManager.LoadScene(sceneName);
    }
    public void LoadHomeScene(string sceneName)
    {
        FindObjectOfType<AudioManager>().Play("Button Click");
        FindObjectOfType<AudioManager>().Stop("Background 2");
        FindObjectOfType<AudioManager>().Play("Background");
        SceneManager.LoadScene(sceneName);
    }
}