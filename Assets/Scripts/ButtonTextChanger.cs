using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextChanger : MonoBehaviour
{
    private TMP_Text buttonText;
    private Button unityButton;
    
    //font for when mouse is not hovering on button
    public TMP_FontAsset NonHover;

    //font for when mouse is hovering on button
    public TMP_FontAsset Hover;

    // Start is called before the first frame update
    void Start()
    {
        // Get the TextMeshPro text component within the children of the current GameObject
        buttonText = GetComponentInChildren<TMP_Text>();
        // Get the Unity UI Button component on the current GameObject
        unityButton = GetComponent<Button>();
        // Set the default font when the mouse is not hovering on the button
        buttonText.font = NonHover;
    }

    void Update()
    {
        // Check if the mouse is hovering over the button and update the font accordingly
        if (IsMouseHoveringOverButton())
        {
            buttonText.font = Hover;// Set the font when the mouse is hovering over the button
        }
        else
        {
            buttonText.font = NonHover;// Set the default font when the mouse is not hovering over the button
        }
    }

    // Check if the mouse is hovering over the Unity button
    private bool IsMouseHoveringOverButton()
    {
        if (unityButton != null)
        {
            // Create a pointer event data with the current mouse position
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            // Check if the mouse is over the button using the EventSystem
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {

                //Debug.Log("Raycast hit: " + result.gameObject.name);

                if (result.gameObject == unityButton.gameObject)
                {
                    // Mouse is hovering over the button
                    return true;
                }
            }
        }

        // Mouse is not hovering over the button
        return false;
    }
}
