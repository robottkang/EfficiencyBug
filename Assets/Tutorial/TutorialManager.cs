using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class TutorialManager : MonoBehaviour
{
    [Header("-In Game Elements")]
    [SerializeField]
    private GameObject selectionBox;
    [SerializeField]
    private GameObject timer;

    [Header("-In Tutorial Elements")]
    [SerializeField]
    private GameObject skipButton;
    [SerializeField]
    private TutorialGuideObjects tutorialGuideObjects;
    private int tutorialOrder = 0;
    private int oldOrder = -1;
    private bool isOrderChanged
    {
        get
        {
            if (oldOrder != tutorialOrder)
            {
                oldOrder = tutorialOrder;
                return true;
            }

            return false;
        }
    }

    private void Update()
    {
        if (isOrderChanged)
        {
            switch (tutorialOrder)
            {
                case 0:
                    tutorialGuideObjects.arrow = Instantiate(tutorialGuideObjects.arrow, GameObject.Find("UI").transform);

                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    //PlayerPrefs.SetInt("Tutorial", 1);

                    Destroy(skipButton);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    //private bool isLockTutoOrder = false;
    //private bool isStart = false;

    //private GameObject _UI;

    //private GameObject UI
    //{
    //    get
    //    {
    //        if (_UI == null)
    //        {
    //            _UI = GameObject.Find("UI");
    //        }

    //        return _UI;
    //    }
    //}

    //private async void Update()
    //{
    //    if (Input.GetMouseButtonUp(0) && !isLockTutoOrder)
    //    {
    //        tutorialOrder++;
    //    }

    //    if (IfIntchanged(tutorialOrder, ref oldOrder) || !isStart)
    //    {
    //        switch (tutorialOrder)
    //        {
    //            case 0:
    //                isStart = true;
    //                Instantiate(skipButton, UI.transform);
    //                tutorialGuideObjects.background = Instantiate(tutorialGuideObjects.background, UI.transform);
    //                tutorialGuideObjects.textBox = Instantiate(tutorialGuideObjects.textBox, UI.transform);
    //                // 반가워요!
    //                break;
    //            case 1:
    //                tutorialGuideObjects.textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 280);
    //                tutorialGuideObjects.textBox.GetComponentInChildren<TextMeshProUGUI>().text = "플레이 방법을\n알려드릴게요";
    //                // 플레이 방법을 알려드릴게요.
    //                break;
    //            case 2:
    //                tutorialGuideObjects.textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 230);
    //                tutorialGuideObjects.textBox.GetComponent<RectTransform>().localPosition = new Vector2(0, 545);
    //                tutorialGuideObjects.textBox.GetComponentInChildren<TextMeshProUGUI>().text = "이것은\n보드입니다";

    //                tutorialGuideObjects.arrow = Instantiate(tutorialGuideObjects.arrow, UI.transform);

    //                tutorialGuideObjects.background.GetComponent<Canvas>().sortingOrder = 5;

    //                selectionBox.SetActive(false);
    //                // 이것은 보드입니다.
    //                break;
    //            case 3:
    //                tutorialGuideObjects.textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 280);
    //                tutorialGuideObjects.textBox.GetComponentInChildren<TextMeshProUGUI>().fontSize = 48;
    //                tutorialGuideObjects.textBox.GetComponentInChildren<TextMeshProUGUI>().text = "스테이지가\n달라질 때마다\n보드의 숫자가\n섞입니다";
    //                // 스테이지가 달라질 때마다 보드의 숫자가 섞입니다.
    //                break;
    //            case 4:
    //                tutorialGuideObjects.textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 280);
    //                tutorialGuideObjects.textBox.GetComponentInChildren<TextMeshProUGUI>().text = "보드를 누르고\n있어보세요!";
    //                isLockTutoOrder = true;
    //                await Task.Delay(5000);
    //                tutorialOrder++;
    //                isLockTutoOrder = false;
    //                // 보드를 누르고 있어보세요!
    //                break;
    //            case 5:
    //                Debug.Log("hello");
    //                // 보드를 누르고 있으면 상자가 나타납니다.
    //                break;
    //            case 6:
    //                // 이 상자 안에 있는 숫자의 총합을 가장 크게 만들면 다음 스테이지로 넘어갑니다. 
    //                break;
    //            case 7:
    //                // 누르고 있는 상태로 상자를 움직일 수 있어요!
    //                break;
    //            case 8:
    //                // 손가락을 때면 결정이 됩니다.
    //                break;
    //            case 9:
    //                // 타이머는 10초의 시간을 가집니다.
    //                break;
    //            case 10:
    //                // 다음 스테이지로 넘어갈 때마다 n초 추가되고,
    //                break;
    //            case 11:
    //                // 최댓값을 고르지 못하였다면, 타이머는 즉시 m초 감소합니다.
    //                break;
    //            case 12:
    //                // 스테이지가 높아질 수록 감소하는 속도가 빨라지므로 빨리빨리 판단하세요!
    //                break;
    //            case 13:
    //                // 이 버튼을 통해 플레이 도중 즉시 리셋이 가능합니다.
    //                break;
    //            case 14:
    //                // 튜토리얼은 설정에서 다시 볼 수 있습니다.
    //                break;
    //            case 15:
    //                // 모든 설명을 끝마쳤습니다.
    //                break;
    //            case 16:
    //                // 당신의 한계에 도전해보세요!
    //                break;
    //            default:
    //                PlayerPrefs.SetInt("Tutorial", 1);

    //                Destroy(gameObject);
    //                PlayerPrefs.SetInt("Tutorial", 1);

    //                Destroy(gameObject);
    //                break;
    //        }
    //    }
    //}

    //private bool IfIntchanged(int current, ref int old)
    //{
    //    if (current != old)
    //    {
    //        old = current;
    //        return true;
    //    }

    //    return false;
    //}

    public void IncreaseTutorialOrder()
    {
        tutorialOrder++;
    }

    public void StartTutorial()
    {
        PlayerPrefs.SetInt("Tutorial", 0);
    }

    public void EndTutorialOrder()
    {
        tutorialOrder = -1;
    }

    [System.Serializable]
    private struct TutorialGuideObjects
    {
        public GameObject background;
        public GameObject textBox;
        public GameObject arrow;
    }
}