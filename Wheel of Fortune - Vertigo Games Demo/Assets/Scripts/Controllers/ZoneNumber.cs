using UnityEngine;
using TMPro;
using DG.Tweening;

public class ZoneNumber : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField] protected TMP_Text numberText;

    protected ZoneController zoneController;
    protected int number;

    public virtual void Initialize(ZoneController controller, int index)
    {
        //Set zoneController if not already initialized
        if(zoneController == null)
            zoneController = controller;

        //Kill any tweening before we set position
        rectTransform.DOKill();

        //Set initial position
        Vector2 anchoredPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = anchoredPos.SetXValue(rectTransform.rect.width * index);

        //Set initial number
        number = index + 1;
        numberText.text = number.ToString();
        SetNumberColor();
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
        SetNumberColor();
    }

    protected virtual void SetNumberColor()
    {
        ZoneController.ZoneColors zoneColors = zoneController.GetZoneColors();
        int superZoneFrequency = zoneController.GetSuperZoneFrequency();
        int safeZoneFrequency = zoneController.GetSafeZoneFrequency();

        if(number % superZoneFrequency == 0)
            numberText.color = zoneColors.superZoneNumberColor;
        else if(number % safeZoneFrequency == 0)
            numberText.color = zoneColors.safeZoneNumberColor;
        else
            numberText.color = zoneColors.zoneNumberColor;
    }
}
