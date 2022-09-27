using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFieldSpace : MonoBehaviour
{
    public int childCount;
    [SerializeField]
    private int _value;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private GameManager gameManager;

    public int value { get => _value; set => _value = value; }
    private TextMeshProUGUI text
    {
        get
        {
            if (_text == null)
            {
                _text = transform.GetComponentInChildren<TextMeshProUGUI>();
            }
            return _text;
        }
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        text.text = value.ToString();
    }

    public void InputValue2GameManager()
    {
        gameManager.inputValue = childCount;
    }
}
