using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RememberCurrentSelection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject lastSelectedElement;

    // Reset is called when attached to a GameObject in the scene
    private void Reset()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.Log("Did not find an Event System in Scene", context: this);
        }

        lastSelectedElement = eventSystem.firstSelectedGameObject;
    }

    private void Start()
    {
        if (!eventSystem)
        {
            Debug.LogError("Event System is not set or found in the scene.", context: this);
            return;
        }
        // Set the first selected element to the first selected GameObject in the Event System
        lastSelectedElement = eventSystem.firstSelectedGameObject;
        if (lastSelectedElement)
        {
            StartCoroutine(TextToSpeakControl.main.SpeakText($"Selected {lastSelectedElement.name}", 0)); // Speak the name of the first selected element
        }
    }

    private void Update()
    {
        if (!eventSystem)
        {
            Debug.LogError("Event System is not set or found in the scene.", context: this);
            return;
        }

        if (eventSystem.currentSelectedGameObject && lastSelectedElement != eventSystem.currentSelectedGameObject)
        {
            lastSelectedElement = eventSystem.currentSelectedGameObject;
            StartCoroutine(TextToSpeakControl.main.SpeakText($"Selected {lastSelectedElement.name}", 0)); // Speak the name of the selected element
        }

        if (!eventSystem.currentSelectedGameObject && lastSelectedElement)
        {
            eventSystem.SetSelectedGameObject(lastSelectedElement);
        }
    }
}
