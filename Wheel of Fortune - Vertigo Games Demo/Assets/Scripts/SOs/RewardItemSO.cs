using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "reward_item", menuName = "Rewards/Reward Item")]
public class RewardItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public int minAmount = 1;
    public int maxAmount = 1000;
    public bool isBomb;
}
