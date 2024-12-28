using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    //TODO: Create Events to handle Button activation/deactivation according to current process
    //Create functions to fire events

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
        int currentZone = zoneController.GetCurrentZone();
        int safeZoneFrequency = zoneController.GetSafeZoneFrequency();
        int superZoneFrequency = zoneController.GetSuperZoneFrequency();

        rewardController.GenerateNewRewards(currentZone, safeZoneFrequency, superZoneFrequency);
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
