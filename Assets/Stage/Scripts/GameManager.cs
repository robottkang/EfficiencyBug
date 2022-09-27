using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int inputValue = 0;
    public int stage = 1;
    public float timeLimit;
    [HideInInspector]
    public FieldBorad field;

    private void Awake()
    {
        InitializeBorad();
    }

    private void Update()
    {
        if (inputValue != 0)
        {
            if (SelectBox(inputValue / 5, inputValue % 5) == FindMaxValue())
            {
                stage++;
                RearrangeBorad(stage);
            }
            else
            {
                FindMaxValue();
            }
            inputValue = 0;
        }
    }

    public int FindMaxValue()
    {
        int maxValue = 0;
        int valueSum = 0;

        for (int boradGlobalX = 1; boradGlobalX < 4; boradGlobalX++)     // 3*3 range center value of x
        {
            for (int boradGlobalY = 1; boradGlobalY < 4; boradGlobalY++) // 3*3 range center value of y
            {
                for (int boradLocalX = boradGlobalX - 1; boradLocalX <= boradGlobalX + 1; boradLocalX++)     // int range [boradGlobalX - 1, boradGlobalX + 1]
                {
                    for (int boradLocalY = boradGlobalY - 1; boradLocalY <= boradGlobalY + 1; boradLocalY++) // int range [boradGlobalY - 1, boradGlobalY + 1]
                    {
                        valueSum += field.field[boradLocalX, boradLocalY].GetComponent<GameFieldSpace>().value;
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


    public int SelectBox(int x, int y)
    {
        int valueSum = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                valueSum += field.field[i, j].GetComponent<GameFieldSpace>().value;
            }
        }

        Debug.Log(valueSum);
        return valueSum;
    }
    public void RearrangeBorad(int stageNum)
    {
        for (int x = 0; x < FieldBorad.columns; x++)
        {
            for (int y = 0; y < FieldBorad.rows; y++)
            {
                field.field[x, y].GetComponent<GameFieldSpace>().value = Random.Range(1, (stageNum + 8) / 5 + 1);
            }
        }
    }

    public void InitializeBorad()
    {
        int spaceCount = 0;
        for (int x = 0; x < FieldBorad.columns; x++)
        {
            for (int y = 0; y < FieldBorad.rows; y++)
            {
                field.field[x, y] = field.fieldObject.transform.GetChild(spaceCount).gameObject;
                field.field[x, y].GetComponent<GameFieldSpace>().childCount = spaceCount;
                spaceCount++;
            }
        }
        RearrangeBorad(stage);
    }
}

[System.Serializable]
public class FieldBorad
{
    public GameObject fieldObject;
    public static int rows = 5;
    public static int columns = 5;
    public GameObject[,] field = new GameObject[columns, rows];
}

public partial class GameSetting
{
    public GameState currentGameState = GameState.NotPlay;
    private bool _whetherDidTutorial;
    public bool whetherDidTutorial
    {
        get
        {
            if (PlayerPrefs.HasKey("Tutorial"))
            {
                return PlayerPrefs.GetInt("Tutorial") > 0 ? true : false;
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