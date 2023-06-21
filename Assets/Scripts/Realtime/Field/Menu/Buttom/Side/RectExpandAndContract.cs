using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectExpandAndContract : MonoBehaviour
{
    [HideInInspector]
    public bool xExpansion, yExpansion;
    [HideInInspector]
    public float xExpansionScale = 1, yExpansionScale = 1;
    [HideInInspector]
    public float expansionSpeed;

    private bool isExpand;
    private MinMax X;
    private MinMax Y;

    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (xExpansion)
        {
            X = new MinMax { min = rectTransform.sizeDelta.x, max = rectTransform.sizeDelta.x * xExpansionScale };
        }
        if (yExpansion)
        {
            Y = new MinMax { min = rectTransform.sizeDelta.y, max = rectTransform.sizeDelta.y * yExpansionScale };
        }
    }

    private void Update()
    {
        ExpandOrContract();
    }

    private void ExpandOrContract()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float x = rectTransform.sizeDelta.x, y = rectTransform.sizeDelta.y;
        float deltaX = (X.max - X.min) * expansionSpeed * Time.deltaTime, deltaY = (Y.max - Y.min) * expansionSpeed * Time.deltaTime;

        if (isExpand)
        {
            if (xExpansion)
            {
                x = Mathf.Clamp(rectTransform.sizeDelta.x + deltaX, X.min, X.max);
            }
            if (yExpansion)
            {
                y = Mathf.Clamp(rectTransform.sizeDelta.y + deltaY, Y.min, Y.max);
            }
        }
        else
        {
            if (xExpansion)
            {
                x = Mathf.Clamp(rectTransform.sizeDelta.x - deltaX, X.min, X.max);
            }
            if (yExpansion)
            {
                y = Mathf.Clamp(rectTransform.sizeDelta.y - deltaY, Y.min, Y.max);
            }

        }

        rectTransform.sizeDelta = new Vector2(x, y);
    }

    public void ExpandOnOff()
    {
        isExpand = !isExpand;
    }

    public struct MinMax
    {
        public float min;
        public float max;
    }
}