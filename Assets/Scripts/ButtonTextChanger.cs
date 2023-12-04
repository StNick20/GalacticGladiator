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
        buttonText = GetComponentInChildren<TMP_Text>();
        unityButton = GetComponent<Button>();
        buttonText.font = NonHover;
    }

    void Update()
    {
        if (IsMouseHoveringOverButton())
        {

            buttonText.font = Hover;
        }
        else
        {
            buttonText.font = NonHover;
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
