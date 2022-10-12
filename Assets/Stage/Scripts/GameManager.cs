using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int inputChildCount = -1;
    private int _stageNum = 0;
    public float timeLimit;
    [HideInInspector]
    public FieldBorad field;
    private Vector2Int _selectionBoxPositionInBorad = new Vector2Int(2,2);

    /// <summary>
    /// stageNum show current stage number (Every time stageNum set bigger than 0 or differently from previous value, currentGameState become Play State).
    /// </summary>
    public int stageNum
    {
        get => _stageNum;
        set
        {
            if (value < 0)
            {
                _stageNum = 0;
                return;
            }

            if (_stageNum != value)
            {
                GameSetting.currentGameState = GameSetting.GameState.Play;
                _stageNum = value;
            }
        }
    }

    /// <summary>
    /// Box can position in range ([1, rows - 1),[1, columns - 1)).
    /// </summary>
    public Vector2Int selectionBoxPositionInBorad
    {
        get => _selectionBoxPositionInBorad;
        set => _selectionBoxPositionInBorad = new Vector2Int(Mathf.Clamp(value.x, 1, FieldBorad.rows - 2), Mathf.Clamp(value.y, 1, FieldBorad.columns - 2));
    }

    private void Awake()
    {
        InitializeBorad();
    }

    private void Update()
    {
        switch (GameSetting.currentGameState)
        {
            case GameSetting.GameState.NotPlay:
                CompareValueWithMaxValue();
                break;
            case GameSetting.GameState.Play:
                CompareValueWithMaxValue();
                break;
            case GameSetting.GameState.Pause:
                break;
            case GameSetting.GameState.Failure:
                break;
        }
    }


    private void CompareValueWithMaxValue()
    {
        if (inputChildCount != -1)
        {
            if (SelectBox(selectionBoxPositionInBorad.x, selectionBoxPositionInBorad.y) == FindMaxValue())
            {
                stageNum++;
                RearrangeBorad(stageNum / 4 + 3);
            }
            inputChildCount = -1;
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
                        valueSum += field.borad[boradLocalX, boradLocalY].GetComponent<GameFieldSpace>().value;
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
                valueSum += field.borad[i, j].GetComponent<GameFieldSpace>().value;
            }
        }

        Debug.Log(valueSum);
        return valueSum;
    }

    /// <summary>
    /// Rearrange value of borad at random.
    /// </summary>
    /// <param name="rangeMax">int range (1, rangeMax)</param>
    public void RearrangeBorad(int rangeMax)
    {
        for (int x = 0; x < FieldBorad.columns; x++)
        {
            for (int y = 0; y < FieldBorad.rows; y++)
            {
                field.borad[x, y].GetComponent<GameFieldSpace>().value = Random.Range(1, rangeMax);
            }
        }
    }

    /// <summary>
    /// initialize borad and rearrange borad value
    /// </summary>
    public void InitializeBorad()
    {
        int spaceCount = 0;
        for (int y = 0; y < FieldBorad.rows; y++)
        {
            for (int x = 0; x < FieldBorad.columns; x++)
            {
                field.borad[x, y] = field.fieldObject.transform.GetChild(spaceCount).gameObject;
                field.borad[x, y].GetComponent<GameFieldSpace>().childCount = spaceCount;
                spaceCount++;
            }
        }
        RearrangeBorad(2);
    }

}

[System.Serializable]
public class FieldBorad
{
    public GameObject fieldObject;
    public static int rows = 5;
    public static int columns = 5;
    public GameObject[,] borad = new GameObject[columns, rows];
}

public static partial class GameSetting
{
    public static GameState currentGameState = GameState.NotPlay;

    public static bool whetherDidTutorial
    {
        get
        {
            if (PlayerPrefs.HasKey("Tutorial"))
            {
                return PlayerPrefs.GetInt("Tutorial") > 0;
            }
            else
            {
                PlayerPrefs.SetInt("Tutorial", 0);
                return false;
            }
        }
    }

    public enum GameState
    {
        NotPlay,
        Play,
        Pause,
        Failure,
    }
}