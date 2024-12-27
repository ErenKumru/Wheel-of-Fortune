using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "reward_library", menuName = "Rewards/Reward Library")]
public class RewardLibrarySO : ScriptableObject
{
    public List<RewardItemSO> rewardItems;
    public RewardItemSO bomb;
}
