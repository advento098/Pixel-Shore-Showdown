using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Array of SoundScript objects to hold all sound settings
    public SoundScript[] sounds;

    // Singleton instance to ensure only one AudioManager exists in the game
    public static AudioManager instance;

    void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
            instance = this;
        else
        {
            // If an instance already exists, destroy this one to enforce the singleton pattern
            Destroy(gameObject);
            return;
        }

        // Prevent the AudioManager from being destroyed when loading new scenes
        DontDestroyOnLoad(gameObject);

        // Initialize each sound in the sounds array
        foreach (SoundScript s in sounds)
        {
            // Add an AudioSource component for each sound and set its properties
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {
        // Automatically play the background music when the game starts
        Play("Background");
    }

    // Function to play a sound by name
    public void Play(string name)
    {
        // Find the sound in the array using its name
        SoundScript s = System.Array.Find(sounds, sound => sound.name == name);

        // If the sound is not found, log a warning and return
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Play the sound
        s.source.Play();
    }

    // Function to stop a sound by name
    public void Stop(string name)
    {
        // Find the sound in the array using its name
        SoundScript s = System.Array.Find(sounds, sound => sound.name == name);

        // If the sound is not found, log a warning and return
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Stop the sound
        s.source.Stop();
    }
}
