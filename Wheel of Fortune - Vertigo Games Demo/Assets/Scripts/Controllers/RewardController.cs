using System.Collections.Generic;
using UnityEngine; 

public class RewardController : MonoBehaviour
{
    [SerializeField] private RewardLibrarySO rewardLibrary;
    [SerializeField] private RewardDisplayer rewardDisplayer;
    [SerializeField] private int currencyId;

    private Reward[] possibleRewards;
    private Dictionary<int, Reward> collectedRewards = new Dictionary<int, Reward>();

    public void Initialize(int reviveCost)
    {
        int maxItemCount = CardGameManager.Instance.wheelController.GetMaxItemCount();
        possibleRewards = new Reward[maxItemCount];
        rewardDisplayer.SetReviveCostText(reviveCost);
    }

    public void GenerateNewRewards(int currentZone, int safeZoneFrequency, int superZoneFrequency)
    {
        int maxItemCount = CardGameManager.Instance.wheelController.GetMaxItemCount();

        //Super Zone, generate special reward items
        if(currentZone % superZoneFrequency == 0)
        {
            GenerateRewards(rewardLibrary.specialRewardItems, maxItemCount, 1);
            return;
        }

        //Generate regular reward items
        GenerateRewards(rewardLibrary.rewardItems, maxItemCount, currentZone);

        //If Safe Zone, do not add bomb 
        //If Regular Zone, add bomb
        if(currentZone % safeZoneFrequency != 0)
        {
            int bombIndex = Random.Range(0, maxItemCount);
            possibleRewards[bombIndex].SetData(rewardLibrary.bomb, 1);
        }
    }

    private void GenerateRewards(List<RewardItemSO> rewardItems, int maxItemCount, int zoneMultiplier)
    {
        for(int i = 0; i < maxItemCount; i++)
        {
            int randomIndex = Random.Range(0, rewardItems.Count);
            RewardItemSO rewardItem = rewardItems[randomIndex];
            int zoneMinAmount = zoneMultiplier * rewardItem.minAmount;
            int zoneMaxAmount = zoneMultiplier * rewardItem.maxAmount;
            int amount = Random.Range(zoneMinAmount, zoneMaxAmount);
            possibleRewards[i].SetData(rewardItem, amount);
        }
    }

    public void SetCollectedReward(int rewardIndex)
    {
        Reward reward = possibleRewards[rewardIndex];
        int rewardId = reward.rewardItem.id;

        //If reward is bomb, nothing to collect
        if(reward.rewardItem.isBomb)
            return;
        
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

        //If reward is bomb, display bomb hit panel
        if(reward.rewardItem.isBomb)
        {
            rewardDisplayer.DisplayBombHitPanel();
            return;
        }

        rewardDisplayer.SetRewardCardData(reward);
        rewardDisplayer.DisplayRewardCard(this, reward);
    }

    public void Progress()
    {
        CardGameManager.Instance.PrepareNextZone();
    }

    public void ClearCollectedRewards()
    {
        rewardDisplayer.ClearCollectionItems();
        collectedRewards.Clear();
    }

    public void HideBombPanel()
    {
        rewardDisplayer.HideBombHitPanel();
    }

    public Reward[] GetPossibleRewards()
    {
        return possibleRewards;
    }

    public int GetCollectedCurrencyAmount()
    {
        if(collectedRewards.TryGetValue(currencyId, out Reward currencyReward))
        {
            return currencyReward.amount;
        }

        return 0;
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
