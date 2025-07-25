using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetUIInteraction : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Selectable uiElement;
    [SerializeField] private EventSystem eventSystem;

    [Header("Visualisation")]
    [SerializeField] private bool showVisualisation;
    [SerializeField] private Color navigationColor = Color.cyan;

    // Draw Gizmos to visualize the interaction path
    private void OnDrawGizmos()
    {
        if (!showVisualisation)
        {
            return;
        }

        if (uiElement == null)
        {
            return;
        }

        Gizmos.color = navigationColor;
        Gizmos.DrawLine(gameObject.transform.position, uiElement.transform.position);
    }

    // Reset is called when attached to a GameObject in the scene
    private void Reset()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.Log("Did not find an Event System in Scene", context: this);
        }
    }

    // Sets the selected UI element when the script is enabled
    public void JumpToElement()
    {
        if (eventSystem == null)
        {
            Debug.LogError("Event System is not set or found in the scene.", context: this);
            return;
        }

        if (uiElement == null)
        {
            Debug.LogError("UI Element is not set.", context: this);
            return;
        }

        eventSystem.SetSelectedGameObject(uiElement.gameObject);
    }

    // Sets the UI element to interact with
    public void SetUiElement(Selectable newUiElement)
    {
        if (newUiElement == null)
        {
            Debug.LogError("New UI Element is null.", context: this);
            return;
        }

        uiElement = newUiElement;
    }
}
