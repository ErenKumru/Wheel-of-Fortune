using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RewardDisplayer : MonoBehaviour
{
    [Header("Reward Card References")]
    [SerializeField] private RectTransform rewardCardRectTransform;
    [SerializeField] private TMP_Text rewardCardNameText;
    [SerializeField] private Image rewardCardIcon;
    [SerializeField] private TMP_Text rewardCardAmountText;
    [SerializeField] private float scaleTime = 0.5f, displayTime = 1f;
    [SerializeField] private Ease scaleEase = Ease.OutCubic;

    public void DisplayRewardCard(RewardController rewardController)
    {
        rewardCardRectTransform.gameObject.SetActive(true);

        rewardCardRectTransform.DOScale(Vector2.one, scaleTime).From(Vector2.zero).SetEase(scaleEase).OnComplete(()=>
        {
            BeginCardDisplayedProcesses(rewardController);
        });
    }

    private void BeginCardDisplayedProcesses(RewardController rewardController)
    {
        rewardController.RegenerateRewards();
        //TODO: Display reward in collection area (maybe jump/shoot it)
        HideRewardCard();
    }

    private void HideRewardCard()
    {
        rewardCardRectTransform.DOScale(Vector2.zero, scaleTime).SetDelay(displayTime).SetEase(scaleEase).OnComplete(() =>
        {
            rewardCardRectTransform.gameObject.SetActive(false);
        });
    }

    public void SetRewardCardData(RewardController.Reward reward)
    {
        rewardCardNameText.text = reward.rewardItem.itemName;
        rewardCardIcon.sprite = reward.rewardItem.icon;
        ResizeCardIconWidth();
        rewardCardAmountText.text = "x" + reward.amount;
    }

    private void ResizeCardIconWidth()
    {
        Vector2 sizeDelta = rewardCardIcon.rectTransform.sizeDelta;
        rewardCardIcon.rectTransform.sizeDelta = sizeDelta.ResizeWidthByTextureSize(rewardCardIcon.sprite.texture);
    }
}
