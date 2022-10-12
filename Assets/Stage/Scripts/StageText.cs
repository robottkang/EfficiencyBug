using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageText : MonoBehaviour
{
    [SerializeField]
    private TextMesh stage;
    [SerializeField]
    private GameManager gameManager;

    private void Update()
    {
        if (GameSetting.currentGameState == GameSetting.GameState.NotPlay)
        {
            stage.text = "효율충";
        }
        else
        {
            stage.text = "stage : " + gameManager.stageNum.ToString();
        }
    }
}
