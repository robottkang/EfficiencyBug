using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Record : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    void Update()
    {
        text.text = "Stage : " + PlayerPrefs.GetInt("HighestScore", 0);
    }
}
