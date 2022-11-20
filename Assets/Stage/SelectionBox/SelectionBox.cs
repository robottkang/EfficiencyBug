using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RectTransform field;
    private bool isTouchBeginInField = false;
    [SerializeField]
    private AudioClip rightChoice;
    [SerializeField]
    private AudioClip wrongChoice;


    private void Initialize()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (field == null)
        {
            field = GameObject.Find("Field").GetComponent<RectTransform>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (GameSetting.currentGameState != GameSetting.GameState.Pause)
        {
            if (GameSetting.currentGameState == GameSetting.GameState.Play || GameSetting.currentGameState == GameSetting.GameState.NotPlay)
            {
                MoveBox();

                SelectTouch();
            }

            animator.SetBool("GameOver", GameSetting.currentGameState == GameSetting.GameState.Over);
        }
    }

    private void SelectTouch()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.position.x <= (field.position.x + field.sizeDelta.x / 2 * transform.root.localScale.x) &&
                touch.position.x >= (field.position.x - field.sizeDelta.x / 2 * transform.root.localScale.x) &&
                touch.position.y <= (field.position.y + field.sizeDelta.y / 2 * transform.root.localScale.y) &&
                touch.position.y >= (field.position.y - field.sizeDelta.y / 2 * transform.root.localScale.y))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    animator.Play("SelectionBoxAppearAni");
                    isTouchBeginInField = true;
                }
                else if (touch.phase == TouchPhase.Ended && isTouchBeginInField)
                {
                    CompareValueWithMaxValue();
                    animator.Play("SelectionBoxDisappearAni");
                    isTouchBeginInField = false;
                }
            }
        }
        else if (isTouchBeginInField)
        {
            animator.Play("SelectionBoxDisappearAni");
            isTouchBeginInField = false;
        }
    }

    private void CompareValueWithMaxValue()
    {
        if (gameManager.stageNum != 0)
        {
            if (!PlayerPrefs.HasKey("CheckStageManipulation"))
            {
                Debug.Log("뭘 어케한거야");
                return;
            }

            if (SecureHelper.HashRobottkangSalt(gameManager.stageNum.ToString()) != PlayerPrefs.GetString("CheckStageManipulation"))
            {
                Debug.Log("니 뭐냐");
                return;
            }
        }

        if (SelectBox(gameManager.selectionBoxPositionInBorad.x, gameManager.selectionBoxPositionInBorad.y) == FindMaxValue())
        {
            PlayAudioClip(rightChoice);
            gameManager.stageNum++;
            PlayerPrefs.SetString("CheckStageManipulation", SecureHelper.HashRobottkangSalt(gameManager.stageNum.ToString()));
            gameManager.RearrangeBorad(gameManager.stageNum / 4 + 3);
        }
        else // not max
        {
            PlayAudioClip(wrongChoice);
            timer.leftTime -= gameManager.penaltyTime;
        }
    }

    public int FindMaxValue()
    {
        int maxValue = 0;
        int valueSum = 0;

        for (int boradGlobalX = 1; boradGlobalX < 4; boradGlobalX++)     // x of 3*3 range center value
        {
            for (int boradGlobalY = 1; boradGlobalY < 4; boradGlobalY++) // y of 3*3 range center value
            {
                for (int boradLocalX = boradGlobalX - 1; boradLocalX <= boradGlobalX + 1; boradLocalX++)     // int range [boradGlobalX - 1, boradGlobalX + 1]
                {
                    for (int boradLocalY = boradGlobalY - 1; boradLocalY <= boradGlobalY + 1; boradLocalY++) // int range [boradGlobalY - 1, boradGlobalY + 1]
                    {
                        valueSum += gameManager.field.borad[boradLocalX, boradLocalY].GetComponent<GameFieldSpace>().value;
                    }
                }

                if (maxValue < valueSum)
                {
                    maxValue = valueSum;
                }

                valueSum = 0;
            }
        }
        return maxValue;
    }

    /// <summary>
    /// set position to borad space's position of selectionBox center
    /// </summary>
    public void MoveBox()
    {
        transform.position = gameManager.field.fieldObject.transform.GetChild(gameManager.selectionBoxPositionInBorad.x + gameManager.selectionBoxPositionInBorad.y * 5).transform.position;
    }

    /// <summary>
    /// return sum value of range 3*3.
    /// </summary>
    /// <param name="x">x of Box's center</param>
    /// <param name="y">y of Box's center</param>
    public int SelectBox(int x, int y)
    {
        int valueSum = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                valueSum += gameManager.field.borad[i, j].GetComponent<GameFieldSpace>().value;
            }
        }

        Debug.Log(valueSum);
        return valueSum;
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}