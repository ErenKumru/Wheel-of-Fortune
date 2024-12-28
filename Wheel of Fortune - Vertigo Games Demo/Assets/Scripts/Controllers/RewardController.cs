using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class RewardController : MonoBehaviour
{
    [SerializeField] private RewardLibrarySO rewardLibrary;
    [SerializeField] private RewardDisplayer rewardDisplayer;

    private Reward[] possibleRewards;
    private Dictionary<int, Reward> collectedRewards = new Dictionary<int, Reward>();

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

    public void SetCollectedReward(int rewardIndex)
    {
        Reward reward = possibleRewards[rewardIndex];
        int rewardId = reward.rewardItem.id;
        
        //Existing reward, increase the amount
        if(collectedRewards.ContainsKey(rewardId))
        {
            if(collectedRewards.TryGetValue(rewardId, out Reward collectedReward))
            {
                collectedReward.amount += reward.amount;
                collectedRewards[rewardId] = collectedReward;
            }
        }
        //New reward, add to collection
        else
        {
            bool success = collectedRewards.TryAdd(rewardId, reward);

            if(!success)
            {
                Debug.LogError("Something went wrong with collectedRewards dictionary! Couldn't add new element!");
            }
        }
    }

    public void DisplayReward(int rewardIndex)
    {
        Reward reward = possibleRewards[rewardIndex];
        rewardDisplayer.SetRewardCardData(reward);
        rewardDisplayer.DisplayRewardCard(this, reward);
    }

    public void Progress()
    {
        CardGameManager.Instance.PrepareNextZone();
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
