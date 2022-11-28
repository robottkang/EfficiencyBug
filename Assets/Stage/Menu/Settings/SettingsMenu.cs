using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TutorialManager tutorialManager;
    [SerializeField]
    private GameManager gameManager;
    private GameSetting.GameState beforeGameState;
    [Header("-Audio")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Sprite volumeOnIcon;
    [SerializeField]
    private Sprite volumeOffIcon;
    [SerializeField]
    private Image SE_LabelIcon;
    [SerializeField]
    private Slider SE_slider;
    [SerializeField]
    private Slider BGM_slider;

    private void Awake()
    {
        float temp;
        audioMixer.SetFloat("SE", Mathf.Log10(temp = PlayerPrefs.GetFloat("SE", 0.1f)) * 20);
        SetVolumeIcon(temp, SE_LabelIcon);
        SE_slider.value = temp;
        audioMixer.SetFloat("BGM", Mathf.Log10(temp = PlayerPrefs.GetFloat("BGM", 0.1f)) * 20);
        BGM_slider.value = temp;
    }

    public void SEControl(float volume)
    {
        PlayerPrefs.SetFloat("SE", volume);
        volume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SE", volume);
        SetVolumeIcon(volume, SE_LabelIcon);
    }

    public void BGMControl(float volume)
    {
        PlayerPrefs.SetFloat("BGM", volume);
        volume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("BGM", volume);
        //SetVolumeIcon(volume, BGM_LabelIcon);
    }

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

    public void SetVolumeIcon(float volume, Image label)
    {
        if (volume < -78)
        {
            label.sprite = volumeOffIcon;
        }
        else
        {
            label.sprite = volumeOnIcon;
        }
    }

    public void ResatrtTuto()
    {
        if (GameObject.Find("TutorialManager(Clone)") == null)
        {
            gameManager.TryChangeHighestScore();
            PlayerPrefs.SetInt("Tutorial", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        beforeGameState = GameSetting.currentGameState;
        
        GameSetting.currentGameState = GameSetting.GameState.Pause;
    }

    private void OnDisable()
    {
        if (beforeGameState == GameSetting.GameState.Over)
        {
            gameManager.Restart();
            return;
        }

        GameSetting.currentGameState = beforeGameState;
    }
}
