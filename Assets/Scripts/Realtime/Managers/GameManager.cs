using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject setting;
    private int _stageNum = 0;
    [SerializeField]
    private TutorialManager tutorialManager;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject quitMenu;
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
            if (_stageNum != value)
            {
                GameSetting.currentGameState = GameSetting.GameState.Play;
            }

            if (value < 0)
            {
                _stageNum = 0;
            }
            else
            {
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


    private void Start()
    {
        Initialize();

        if (PlayerPrefs.HasKey("HighestScore"))
        {
            TryChangeHighestScore();
        }
        else
        {
            SetHighestScore();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitMenu.SetActive(true);
        }

        switch (GameSetting.currentGameState)
        {
            case GameSetting.GameState.NotPlay:
                GameSetting.getsContinueReward = false;
                break;
            case GameSetting.GameState.Play:
                break;
            case GameSetting.GameState.Pause:
                break;
            case GameSetting.GameState.Over:
                HandleOver();
                break;
        }
    }

    private void HandleOver()
    {
        TryChangeHighestScore();

        if (!GameSetting.getsContinueReward)
        {
            GameSetting.getsContinueReward = true;
            continueButton.SetActive(true);
        }
    }

    public void TryChangeHighestScore()
    {
        if (stageNum > PlayerPrefs.GetInt("HighestScore", 0))
        {
            PlayerPrefs.SetInt("HighestScore", stageNum);
        }
    }

    private void SetHighestScore()
    {
        PlayerPrefs.SetInt("HighestScore", stageNum);
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

    public void Restart()
    {
        TryChangeHighestScore();
        stageNum = 0;
        GameSetting.currentGameState = GameSetting.GameState.NotPlay;
        GameSetting.getsContinueReward = false;
        RearrangeBorad(2);
    }

    public void ContinueGame()
    {
        GameSetting.currentGameState = GameSetting.GameState.Play;
        FindObjectOfType<Timer>().ResetTimer();
    }

    private void Initialize()
    {
        InitializeBorad();
        InitializeMobileAd();

        if (!GameSetting.whetherDidTutorial)
        {
            Instantiate(tutorialManager.gameObject).GetComponent<TutorialManager>().gameManager = this;
        }
    }

    /// <summary>
    /// initialize borad and rearrange borad value
    /// </summary>
    public void InitializeBorad()
    {
        setting.SetActive(false);

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

    private void InitializeMobileAd()
    {
        MobileAds.Initialize(initStatus => { });

        GameSetting.adUnitId = "";

        LoadRewardedAd();
    }
    
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (GameSetting.rewardedAd != null)
        {
            GameSetting.rewardedAd.Destroy();
            GameSetting.rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(GameSetting.adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                GameSetting.rewardedAd = ad;
            });
    }
    
    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (GameSetting.rewardedAd != null && GameSetting.rewardedAd.CanShowAd())
        {
            GameSetting.rewardedAd.Show((Reward reward) =>
            {
                ContinueGame();
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
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
            return PlayerPrefs.GetInt("Tutorial", 0) > 0;
        }
    }

    public enum GameState
    {
        NotPlay,
        Play,
        Pause,
        Over,
    }

    public static RewardedAd rewardedAd;
    public static string adUnitId;
    public static bool getsContinueReward = false;
}