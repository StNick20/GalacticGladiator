using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BlackAndWhiteToggleHandler : MonoBehaviour
{
    
    public Toggle blackAndWhiteToggle;
    public PostProcessVolume postProcessVolume;
    private ColorGrading colourGrading;

    void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out colourGrading);

        // Load the black and white toggle state from PlayerPrefs or GameManager
        bool isBlackAndWhiteEnabled;
        if (PlayerPrefs.GetInt("IsBlackAndWhiteEnabled", 0) == 1)
        {
            isBlackAndWhiteEnabled = true;
        }
        else
        {
            isBlackAndWhiteEnabled = false;
        }
        GameManager.Instance.IsBlackAndWhiteEnabled = isBlackAndWhiteEnabled;

        //debugs to check values
        Debug.Log("New Scene Loaded");
        Debug.Log("PlayerPrefs Value: " + PlayerPrefs.GetInt("IsBlackAndWhiteEnabled", 0));
        Debug.Log("GameManager Value: " + GameManager.Instance.IsBlackAndWhiteEnabled);
        
        //only runs if the toggle is available as some scenes do not include the toggle
        if (blackAndWhiteToggle != null)
        {
            blackAndWhiteToggle.isOn = false;
            blackAndWhiteToggle.onValueChanged.AddListener(ToggleBlackAndWhiteEffect);
            
        }
        ApplyBlackAndWhiteEffect();
    }

    void ToggleBlackAndWhiteEffect(bool isOn)
    {
        GameManager.Instance.IsBlackAndWhiteEnabled = isOn;
        ApplyBlackAndWhiteEffect();

        //Sace the black and white toggle state to PlayerPrefs or GameManager
        PlayerPrefs.SetInt("IsBlackAndWhiteEnabled", isOn ? 1 : 0);
        Debug.Log("PlayerPrefs Value: " + PlayerPrefs.GetInt("IsBlackAndWhiteEnabled", 0));
    }

    void ApplyBlackAndWhiteEffect()
    {
        colourGrading.enabled.Override(GameManager.Instance.IsBlackAndWhiteEnabled);
    }
}
