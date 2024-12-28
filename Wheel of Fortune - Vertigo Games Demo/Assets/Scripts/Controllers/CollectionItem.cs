using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CollectionItem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemAmountText;

    [Header("Text Animation Values")]
    [SerializeField] private bool animateText = true;
    [SerializeField] private float animateTime = 1f;
    [SerializeField] private Ease animateEase = Ease.OutQuad;

    private int currentAmount;

    public void DisplayItem(RewardController.Reward reward)
    {
        itemIcon.sprite = reward.rewardItem.icon;
        ResizeIconHeight();
        UpdateItemAmount(reward);
    }

    public void UpdateItemAmount(RewardController.Reward reward)
    {
        currentAmount += reward.amount;
        UpdateText(reward.amount, animateText);
    }

    private void UpdateText(int rewardAmount, bool animate)
    {
        if(animate)
        {
            int amount = currentAmount - rewardAmount;

            DOTween.To(() => amount, x => amount = x, currentAmount, animateTime).SetEase(animateEase).OnUpdate(() =>
            {
                itemAmountText.text = amount.ToString();
            });
        }
        else
        {
            itemAmountText.text = currentAmount.ToString();
        }
    }

    private void ResizeIconHeight()
    {
        Vector2 sizeDelta = itemIcon.rectTransform.sizeDelta;
        itemIcon.rectTransform.sizeDelta = sizeDelta.ResizeHeightByTextureSize(itemIcon.sprite.texture);
    }
}
