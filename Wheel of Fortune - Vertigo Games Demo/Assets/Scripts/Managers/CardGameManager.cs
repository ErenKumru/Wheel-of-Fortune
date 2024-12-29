using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    public static event Action OnSpinWheel;
    public static event Action OnZonePrepared;

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

        TriggerOnZonePrepared();
    }

    public void CollectReward(int rewardIndex)
    {
        rewardController.SetCollectedReward(rewardIndex);
    }

    public void DisplayCollectedReward(int rewardIndex)
    {
        rewardController.DisplayReward(rewardIndex);
    }

    public void TriggerOnSpinWheel()
    {
        OnSpinWheel?.Invoke();
    }

    public void TriggerOnZonePrepared()
    {
        OnZonePrepared?.Invoke();
    }
}
