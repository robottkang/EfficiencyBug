using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButton : MonoBehaviour
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
        if (beforeGameState != GameSetting.currentGameState && GameSetting.currentGameState != GameSetting.GameState.Pause)
        {
            if (GameSetting.currentGameState == GameSetting.GameState.Over)
            {
                animator.SetTrigger("Disappear");
            }
            else if (GameSetting.currentGameState == GameSetting.GameState.NotPlay)
            {
                animator.SetTrigger("Appear");
            }

            beforeGameState = GameSetting.currentGameState;
        }
    }
}