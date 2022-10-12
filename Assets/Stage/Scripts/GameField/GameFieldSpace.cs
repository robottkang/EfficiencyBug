using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GameFieldSpace : MonoBehaviour, IPointerEnterHandler
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
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        text.text = value.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameManager.selectionBoxPositionInBorad = new Vector2Int(childCount % FieldBorad.rows, childCount / FieldBorad.columns);
    }
    
    public void InputValue2GameManager()
    {
        gameManager.inputChildCount = childCount;
    }
}
