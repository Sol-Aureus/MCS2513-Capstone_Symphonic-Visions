using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine;

public class TextToSpeakControl : MonoBehaviour
{
    // Makes functions easy to access
    public static TextToSpeakControl main;

    [Header("Text to Speech Speaker")]
    [SerializeField] private TTSSpeaker textSpeaker;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // SpeakText method to trigger the text-to-speech functionality
    public IEnumerator SpeakText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        textSpeaker.Speak(text);
    }
}
