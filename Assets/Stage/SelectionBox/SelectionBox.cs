using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private RectTransform _field;
    private RectTransform field
    {
        get
        {
            if (_field == null)
            {
                _field = GameObject.Find("Field").GetComponent<RectTransform>();
            }
            return _field;
        }
    }


    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if (GameSetting.currentGameState == GameSetting.GameState.Play || GameSetting.currentGameState == GameSetting.GameState.NotPlay)
        {
            //set position to borad space's position of selectionBox center
            transform.position = gameManager.field.fieldObject.transform.GetChild(gameManager.selectionBoxPositionInBorad.x + gameManager.selectionBoxPositionInBorad.y * 5).transform.position;

            SelectTouch();
        }
    }

    private void SelectTouch()
    {
        Color color = GetComponent<Image>().color;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x <= (field.position.x + field.sizeDelta.x / 2) &&
                touch.position.x >= (field.position.x - field.sizeDelta.x / 2) &&
                touch.position.y <= (field.position.y + field.sizeDelta.y / 2) &&
                touch.position.y >= (field.position.y - field.sizeDelta.y / 2))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    gameManager.inputChildCount = gameManager.selectionBoxPositionInBorad.x + gameManager.selectionBoxPositionInBorad.y * 5;
                }
            }
        }
        else
        {
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
        }
    }
}
