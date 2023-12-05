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

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out colourGrading);

        // Load the black and white toggle state from PlayerPrefs or GameManager
        bool isBlackAndWhiteEnabled = PlayerPrefs.GetInt("IsBlackAndWhiteEffect", 0) == 1 ? true : false;
        GameManager.Instance.IsBlackAndWhiteEnabled = isBlackAndWhiteEnabled;

        if (blackAndWhiteToggle != null)
        {
            blackAndWhiteToggle.isOn = false;
            blackAndWhiteToggle.onValueChanged.AddListener(ToggleBlackAndWhiteEffect);
            ApplyBlackAndWhiteEffect();
        }
    }

    void ToggleBlackAndWhiteEffect(bool isOn)
    {
        GameManager.Instance.IsBlackAndWhiteEnabled = isOn;
        ApplyBlackAndWhiteEffect();

        //Sace the black and white toggle state to PlayerPrefs or GameManager
        PlayerPrefs.SetInt("IsBlackAndWhiteEnabled", isOn ? 1 : 0);
    }

    void ApplyBlackAndWhiteEffect()
    {
        colourGrading.enabled.Override(GameManager.Instance.IsBlackAndWhiteEnabled);
    }

}
