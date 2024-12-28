using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    [Header("Controller References")]
    public ZoneController zoneController;
    public WheelController wheelController;
    public RewardController rewardController;

    protected override void AwakeSingleton()
    {
        zoneController.Initialize();
        rewardController.Initialize();
    }

    private void Start()
    {
        GenerateRewards();
    }

    public void PrepareNextZone()
    {
        zoneController.ProgressZone();
        GenerateRewards();
    }

    public void GenerateRewards()
    {
        rewardController.GenerateNewRewards();
        wheelController.DisplayNewRewards(rewardController.GetPossibleRewards());
    }

    public void CollectReward(int rewardIndex)
    {
        rewardController.SetCollectedReward(rewardIndex);
    }

    public void DisplayCollectedReward(int rewardIndex)
    {
        rewardController.DisplayReward(rewardIndex);
    }
}
