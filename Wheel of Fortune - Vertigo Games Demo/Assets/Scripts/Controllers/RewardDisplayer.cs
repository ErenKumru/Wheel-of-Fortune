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

    [Header("Collection References")]
    [SerializeField] private CollectionItem collectionItemPrefab;
    [SerializeField] private RectTransform collectionItemsParent;
    private Dictionary<int, CollectionItem> collectionItems = new Dictionary<int, CollectionItem>();

    public void DisplayRewardCard(RewardController rewardController, RewardController.Reward reward)
    {
        rewardCardRectTransform.gameObject.SetActive(true);

        rewardCardRectTransform.DOScale(Vector2.one, scaleTime).From(Vector2.zero).SetEase(scaleEase).OnComplete(()=>
        {
            BeginCardDisplayedProcesses(rewardController, reward);
        });
    }

    private void BeginCardDisplayedProcesses(RewardController rewardController, RewardController.Reward reward)
    {
        rewardController.Progress();
        AddRewardToCollection(reward);
        HideRewardCard();
    }

    private void AddRewardToCollection(RewardController.Reward reward)
    {
        int rewardId = reward.rewardItem.id;

        //Existing reward, update amount
        if(collectionItems.ContainsKey(rewardId))
        {
            if(collectionItems.TryGetValue(rewardId, out CollectionItem collectionItem))
            {
                collectionItem.UpdateItemAmount(reward);
            }
            else
            {
                Debug.LogError("Something went wrong with collectionItems dictionary! Couldn't find element!");
            }
        }
        //New reward, add to collection and display
        else
        {
            CollectionItem collectionItem = Instantiate(collectionItemPrefab, collectionItemsParent);
            collectionItem.transform.SetAsFirstSibling();
            collectionItem.DisplayItem(reward);

            bool addedToCollection = collectionItems.TryAdd(rewardId, collectionItem);

            if(!addedToCollection)
            {
                Debug.LogError("Something went wrong with collectionItems dictionary! Couldn't add new element!");
            }
        }
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

        if(reward.rewardItem.isBomb)
            rewardCardAmountText.text = string.Empty;
        else
            rewardCardAmountText.text = "x" + reward.amount;
    }

    private void ResizeCardIconWidth()
    {
        Vector2 sizeDelta = rewardCardIcon.rectTransform.sizeDelta;
        rewardCardIcon.rectTransform.sizeDelta = sizeDelta.ResizeWidthByTextureSize(rewardCardIcon.sprite.texture);
    }
}
