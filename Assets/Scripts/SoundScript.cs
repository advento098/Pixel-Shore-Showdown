using UnityEngine;

// The SoundScript class is used to define and manage the properties of individual sounds in the game.
[System.Serializable]
public class SoundScript
{
    // The name of the sound, used to identify it in the AudioManager
    public string name;

    // The audio clip to be played
    public AudioClip clip;

    // The volume of the sound, adjustable between 0 (silent) and 1 (full volume)
    [Range(0f, 1f)]
    public float volume;

    // The pitch of the sound, adjustable between 0 (low) and 1 (high)
    [Range(0f, 1f)]
    public float pitch;

    // Determines whether the sound should loop when played
    public bool loop;

    // The AudioSource component used to play the sound
    // This is hidden in the inspector since it's managed by the AudioManager
    [HideInInspector]
    public AudioSource source;
}
