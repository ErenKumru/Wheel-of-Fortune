using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "reward_item", menuName = "Rewards/Reward Item")]
public class RewardItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public bool isBomb;
}
