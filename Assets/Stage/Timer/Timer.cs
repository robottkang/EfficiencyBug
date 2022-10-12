using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Slider timerSlider;
    [SerializeField]
    private float bonusTime;
    private int stageNumBeforeChangeing;
    private float _leftTime;

    /// <summary>
    /// leftTime show the time left from 0 and set currentGameState to Filure State when it become 0.
    /// </summary>
    public float leftTime
    {
        get => _leftTime;
        private set
        {
            _leftTime = Mathf.Clamp(value, 0, gameManager.timeLimit);
            
            if (_leftTime == 0f)
            {
                GameSetting.currentGameState = GameSetting.GameState.Failure;
                Debug.Log("X(");
            }
        }
    }
    /// <summary>
    /// decreaseSpeed return x / sqrt(1 + pow(x, 2)) + 2f (however, x = stageNum / 16f - 3)
    /// </summary>
    public float decreaseSpeed
    {
        get
        {
            float x = gameManager.stageNum / 16f - 3f;
            //Debug.Log(x / Mathf.Sqrt(1 + Mathf.Pow(x, 2)) + 2f);
            return x / Mathf.Sqrt(1 + Mathf.Pow(x, 2)) + 2f;
        }
    }

    private void Awake()
    {
        stageNumBeforeChangeing = gameManager.stageNum;
        leftTime = gameManager.timeLimit;
    }

    private void Update()
    {
        switch (GameSetting.currentGameState)
        {
            case GameSetting.GameState.Play:
                if (stageNumBeforeChangeing != gameManager.stageNum)
                {
                    leftTime += bonusTime;
                    stageNumBeforeChangeing = gameManager.stageNum;
                }

                timerSlider.value = Mathf.Clamp01((leftTime -= Time.deltaTime * decreaseSpeed) / gameManager.timeLimit);
                break;
            case GameSetting.GameState.NotPlay:
                timerSlider.value = leftTime = gameManager.timeLimit;
                break;
            default:
                break;
        }
    }
}
