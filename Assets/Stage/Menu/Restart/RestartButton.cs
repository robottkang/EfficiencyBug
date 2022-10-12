using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Animator animator;
    private GameSetting.GameState beforeGameState = GameSetting.currentGameState;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if (beforeGameState != GameSetting.currentGameState)
        {
            if (GameSetting.currentGameState == GameSetting.GameState.Play)
            {
                animator.SetTrigger("Appear");
            }
            else if (GameSetting.currentGameState == GameSetting.GameState.NotPlay)
            {
                animator.SetTrigger("Disappear");
            }

            beforeGameState = GameSetting.currentGameState;
        }
    }

    public void Restart()
    {
        gameManager.stageNum = 0;
        GameSetting.currentGameState = GameSetting.GameState.NotPlay;
        gameManager.RearrangeBorad(2);
    }
}