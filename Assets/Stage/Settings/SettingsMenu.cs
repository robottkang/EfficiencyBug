using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("-Audio")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider BGMSlider;
    [SerializeField]
    private Slider SESlider;

    public void SetQuality(int qualityIndex)
    {
        switch (qualityIndex)
        {
            case 0:
                qualityIndex = 3;
                break;
            case 1:
                qualityIndex = 2;
                break;
            case 2:
                qualityIndex = 1;
                break;
            default:
                qualityIndex = 3;
                break;
        }
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
