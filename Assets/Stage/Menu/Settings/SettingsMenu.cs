using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private GameSetting.GameState beforeGameState;
    [Header("-Audio")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider SE_slider;
    [SerializeField]
    private Slider BGM_slider;

    private void Awake()
    {
        float temp;
        audioMixer.SetFloat("SE", Mathf.Log10(temp = PlayerPrefs.GetFloat("SE", 0.1f)) * 20);
        SE_slider.value = temp;
        audioMixer.SetFloat("BGM", Mathf.Log10(temp = PlayerPrefs.GetFloat("BGM", 0.1f)) * 20);
        BGM_slider.value = temp;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RectTransform UI = transform.root.GetComponent<RectTransform>();
            RectTransform rectTransform = transform.GetComponent<RectTransform>();

            if (touch.position.x > (transform.position.x + rectTransform.sizeDelta.x / UI.sizeDelta.x * Screen.currentResolution.width / 2) ||
                touch.position.x < (transform.position.x - rectTransform.sizeDelta.x / UI.sizeDelta.x * Screen.currentResolution.width / 2) ||
                touch.position.y > (transform.position.y + rectTransform.sizeDelta.y / UI.sizeDelta.x * Screen.currentResolution.height / 2) ||
                touch.position.y < (transform.position.y - rectTransform.sizeDelta.y / UI.sizeDelta.x * Screen.currentResolution.height / 2))
            {
                gameObject.SetActive(false);
            }

        }
    }

    public void SEControl(float volume)
    {
        PlayerPrefs.SetFloat("SE", volume);
        volume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SE", volume);
    }

    public void BGMControl(float volume)
    {
        PlayerPrefs.SetFloat("BGM", volume);
        volume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("BGM", volume);
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
