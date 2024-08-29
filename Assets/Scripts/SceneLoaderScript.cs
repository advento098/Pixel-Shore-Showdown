using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    // Function to load a scene, typically the gameplay scene
    public void LoadPlayScene(string sceneName)
    {
        // Play the button click sound effect
        FindObjectOfType<AudioManager>().Play("Button Click");

        // Stop the current background music
        FindObjectOfType<AudioManager>().Stop("Background");

        // Play the alternate background music for the gameplay scene
        FindObjectOfType<AudioManager>().Play("Background 2");

        // Load the specified scene (e.g., the gameplay scene)
        SceneManager.LoadScene(sceneName);
    }

    // Function to load the home scene (main menu)
    public void LoadHomeScene(string sceneName)
    {
        // Play the button click sound effect
        FindObjectOfType<AudioManager>().Play("Button Click");

        // Stop the gameplay background music
        FindObjectOfType<AudioManager>().Stop("Background 2");

        // Play the original background music for the home scene
        FindObjectOfType<AudioManager>().Play("Background");

        // Load the specified scene (e.g., the main menu or home scene)
        SceneManager.LoadScene(sceneName);
    }
}
