using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WheelRewardSlot : MonoBehaviour
{
    [SerializeField] private Image rewardIcon;
    [SerializeField] private TMP_Text rewardAmountText;

    public void SetSlot(Sprite icon, int amount)
    {
        rewardIcon.sprite = icon;

        if(amount > 1)
            rewardAmountText.text = "x" + amount;
        else
            rewardAmountText.text = string.Empty;
    }
}
