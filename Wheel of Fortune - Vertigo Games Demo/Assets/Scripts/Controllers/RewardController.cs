using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    [SerializeField] private RewardLibrarySO rewardLibrary;

    private Reward[] possibleRewards;

    public void Initialize()
    {
        int maxItemCount = CardGameManager.Instance.wheelController.GetMaxItemCount();
        possibleRewards = new Reward[maxItemCount];
    }

    public void GenerateNewRewards()
    {
        int maxItemCount = CardGameManager.Instance.wheelController.GetMaxItemCount();

        for(int i = 0; i < maxItemCount; i++)
        {
            int randomIndex = Random.Range(0, rewardLibrary.rewardItems.Count);
            RewardItemSO rewardItem = rewardLibrary.rewardItems[randomIndex];
            int amount = Random.Range(1, 100);
            possibleRewards[i].SetData(rewardItem, amount);
        }

        int bombIndex = Random.Range(0, maxItemCount);
        possibleRewards[bombIndex].SetData(rewardLibrary.bomb, 1);
    }

    public Reward[] GetPossibleRewards()
    {
        return possibleRewards;
    }

    public struct Reward
    {
        public RewardItemSO rewardItem;
        public int amount;

        public void SetData(RewardItemSO rewardItem, int amount)
        {
            this.rewardItem = rewardItem;
            this.amount = amount;
        }
    }
}
