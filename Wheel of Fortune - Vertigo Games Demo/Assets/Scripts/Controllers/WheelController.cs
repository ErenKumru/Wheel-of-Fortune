using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class WheelController : MonoBehaviour
{
    [Header("Wheel References")]
    [SerializeField] private Image wheelImage;
    [SerializeField] private int lastSelectionIndex = 0;
    [SerializeField] private int maxItemCount = 8;
    [SerializeField] private int minRotation = 4, maxRotation = 12;
    [SerializeField] private float rotationTime = 2f;
    [SerializeField] private Ease easeMode = Ease.OutQuart;

    [Header("Wheel Slot References")]
    [SerializeField] private WheelRewardSlot[] wheelRewardSlots;

    public void SelectReward()
    {
        //Select index
        int selectionIndex = Random.Range(0, maxItemCount);

        //Collect Reward
        CardGameManager.Instance.CollectReward(selectionIndex);

        //Calculate rotation amount
        Vector3 rotation = CalculateWheelRotation(selectionIndex);

        //Update last selection index   
        lastSelectionIndex = selectionIndex;

        //Spin wheel
        SpinWheel(selectionIndex, rotation);

        //Notify wheel spinned
        CardGameManager.Instance.TriggerOnSpinWheel();
    }

    private Vector3 CalculateWheelRotation(int selectionIndex)
    {
        float remainingRotationRatio = 1f - lastSelectionIndex / (float)maxItemCount;
        float selectionRotationRatio = selectionIndex / (float)maxItemCount;
        int randomFullRotation = Random.Range(minRotation, maxRotation + 1);
        float totalRotationAmount = (remainingRotationRatio + selectionRotationRatio + randomFullRotation) * 360;
        Vector3 rotation = wheelImage.rectTransform.localRotation.eulerAngles;
        rotation.z += totalRotationAmount;
        return rotation;
    }

    private void SpinWheel(int selectionIndex, Vector3 rotation)
    {
        wheelImage.rectTransform.DOLocalRotate(rotation, rotationTime, RotateMode.FastBeyond360).SetEase(easeMode).OnComplete(() =>
        {
            CardGameManager.Instance.DisplayCollectedReward(selectionIndex);
        });
    }

    public void DisplayNewRewards(RewardController.Reward[] possibleRewards)
    {
        for(int i = 0; i < maxItemCount; i++)
        {
            RewardController.Reward possibleReward = possibleRewards[i];
            wheelRewardSlots[i].SetSlot(possibleReward.rewardItem.icon, possibleReward.amount);
        }
    }

    public int GetMaxItemCount()
    {
        return maxItemCount;
    }
}
