using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("-In Game Elements")]
    public SelectionBox selectionBox;
    public Timer timer;
    public TextMeshProUGUI title;

    [Space(10)]
    [SerializeField]
    private string signParent;
    [SerializeField]
    private GameObject maxValueSignObject;
    private int stageCheckInt = 1;
    private Dictionary<int, string> titleSummaryText = new Dictionary<int, string>
    {
        { 1, "Find Max" },
        { 2, "Good Job!" },
        { 3, "Well done!" }
    };
    private List<GameObject> maxValueSigns = new List<GameObject>();


    private void Awake()
    {
        Initialize();

        title.GetComponent<StageText>().enabled = false;
        title.text = "Welcome";
        title.fontSize = 80;
    }

    private void Update()
    {
        if (gameManager.stageNum >= 4)
        {
            title.GetComponent<StageText>().enabled = true;
            timer.stopTimer = false;
            title.fontSize = 100;
            title.text = "stage : 4";

            DestroyGuideSigns();
            Destroy(gameObject);
            return;
        }

        if (gameManager.stageNum == stageCheckInt)
        {
            DestroyGuideSigns();

            InstantiateMaxValueSign();

            title.text = titleSummaryText[stageCheckInt++];
        }
    }

    void DestroyGuideSigns()
    {
        foreach (GameObject destoryObject in maxValueSigns)
        {
            Destroy(destoryObject);
        }
        maxValueSigns.Clear();
    }

    private void InstantiateMaxValueSign()
    {
        int maxValue = selectionBox.FindMaxValue();

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

                if (maxValue == valueSum)
                {
                    maxValueSigns.Add(Instantiate(maxValueSignObject, gameManager.field.borad[boradGlobalX, boradGlobalY].transform.position, Quaternion.identity, GameObject.Find(signParent).transform));
                    return; // i want to sign all of sign point, but i cant come up with how to distinguish sign. (random color is not what i want)
                }

                valueSum = 0;
            }
        }
    }

    private void Initialize()
    {
        gameManager = FindObjectOfType<GameManager>();
        selectionBox = FindObjectOfType<SelectionBox>();
        timer = FindObjectOfType<Timer>();
        title = FindObjectOfType<StageText>().GetComponent<TextMeshProUGUI>();

        timer.ResetTimer();
        timer.stopTimer = true;

        PlayerPrefs.SetInt("Tutorial", 1);
        PlayerPrefs.Save();
    }
}