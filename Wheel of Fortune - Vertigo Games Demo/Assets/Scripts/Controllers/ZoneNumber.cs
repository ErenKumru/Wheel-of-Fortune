using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ZoneNumber : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField] private TMP_Text numberText;

    private int number;

    public virtual void Initialize(int index)
    {
        //Kill any tweening before we set position
        rectTransform.DOKill();

        //Set initial position
        Vector2 anchoredPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = anchoredPos.SetXValue(rectTransform.rect.width * index);

        //Set initial number
        number = index + 1;
        numberText.text = number.ToString();
    }

    public virtual void Progress(int zoneNumberCount, float startPosX, float endPosX, float moveTime, float delay)
    {
        float width = rectTransform.rect.width;

        rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - width, moveTime).SetDelay(delay).OnComplete(()=>
        {
            float endPosBoundary = endPosX - width / 2;

            //Loop to start position if the boundary is exceeded
            if(rectTransform.anchoredPosition.x < endPosBoundary)
                LoopToStart(zoneNumberCount, startPosX);
        });
    }

    protected virtual void LoopToStart(int zoneNumberCount, float startPosX)
    {
        rectTransform.anchoredPosition = new Vector2(startPosX, 0);

        //Update number
        number += zoneNumberCount;
        numberText.text = number.ToString();
    }
}
