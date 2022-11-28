using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stage;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private string title;
    
    private GameSetting.GameState rightBeforeGameState = GameSetting.GameState.NotPlay;
    private GameSetting.GameState beforeGameState = GameSetting.GameState.NotPlay;

    private void Update()
    {
        if (rightBeforeGameState != GameSetting.currentGameState)
        {
            beforeGameState = rightBeforeGameState;
            rightBeforeGameState = GameSetting.currentGameState;
        }

        if (GameSetting.currentGameState == GameSetting.GameState.NotPlay || (GameSetting.currentGameState == GameSetting.GameState.Pause && beforeGameState == GameSetting.GameState.NotPlay))
        {
            stage.text = title;
        }
        else
        {
            stage.text = "stage : " + gameManager.stageNum.ToString();
        }
    }
}
