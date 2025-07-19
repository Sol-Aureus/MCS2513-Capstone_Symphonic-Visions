using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip volumeChange;

    // Start is called before the first frame update
    private void Start()
    {
        SetMasterVolume(8); // Set default master volume to 8 (out of 10)
        SetSoundVolume(8); // Set default sound effects volume to 8 (out of 10)
        SetMusicVolume(8); // Set default music volume to 8 (out of 10)
        SetNarrationVolume(8); // Set default narration volume to 8 (out of 10)
    }

    // Method to set the master volume level
    public void SetMasterVolume(float level)
    {
        // Convert 0 to 10 range to -80 to 0 dB range
        level = Mathf.Log10((level + 0.00001f) / 10) * 20;
        audioMixer.SetFloat("MasterVolume", level);

        SoundFXManager.main.PlaySound(volumeChange, transform, 1f); // Play sound effect when volume changes
    }

    // Method to set the sound effects volume level
    public void SetSoundVolume(float level)
    {
        // Convert 0 to 10 range to -80 to 0 dB range
        level = Mathf.Log10((level + 0.00001f) / 10) * 20;
        audioMixer.SetFloat("SoundEffectsVolume", level);

        SoundFXManager.main.PlaySound(volumeChange, transform, 1f); // Play sound effect when volume changes
    }

    // Method to set the music volume level
    public void SetMusicVolume(float level)
    {
        // Convert 0 to 10 range to -80 to 0 dB range
        level = Mathf.Log10((level + 0.00001f) / 10) * 20;
        audioMixer.SetFloat("MusicVolume", level);
    }

    // Method to set the narration volume level
    public void SetNarrationVolume(float level)
    {
        // Convert 0 to 10 range to -80 to 0 dB range
        level = Mathf.Log10((level + 0.00001f) / 10) * 20;
        audioMixer.SetFloat("NarrationVolume", level);

        StartCoroutine(TextToSpeakControl.main.SpeakText("Volume Changed!", 0)); // Speak the name of the selected element
    }
}
