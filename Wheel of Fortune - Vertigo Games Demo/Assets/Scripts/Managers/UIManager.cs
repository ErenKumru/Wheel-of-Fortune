using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private TMP_Text currencyText;

    [Header("Warning Panel References")]
    [SerializeField] private RectTransform currencyWarningPanelRectTransform;
    [SerializeField] private float scaleTime = 1f;
    [SerializeField] private float displayTime = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutCubic;

    public void OpenGameCanvas()
    {
        menuCanvas.enabled = false;
        gameCanvas.enabled = true;
    }

    public void OpenMenuCanvas()
    {
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void UpdateCurrencyText(int amount)
    {
        currencyText.text = amount.ToString();
    }

    public void DisplayCurrencyWarning()
    {
        currencyWarningPanelRectTransform.gameObject.SetActive(true);
        currencyWarningPanelRectTransform.DOKill();

        currencyWarningPanelRectTransform.DOScale(Vector2.one, scaleTime).From(Vector2.zero).SetEase(easeType).OnComplete(()=>
        {
            currencyWarningPanelRectTransform.DOScale(Vector2.zero, scaleTime).SetDelay(displayTime).SetEase(easeType).OnComplete(() =>
            {
                currencyWarningPanelRectTransform.gameObject.SetActive(false);
            });
        });
    }
}
