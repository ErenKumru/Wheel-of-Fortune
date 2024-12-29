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

    //We can use a CurrencyManager but it is really not necessary for this simple of a currency management
    //If it gets more complex/complicated we can easily create a manager and move currency management to there
    [Header("Currency Values")]
    [SerializeField] private int currentCurrency = 100;
    [SerializeField] private int reviveCost = 25;
    private const string currencySaveID = "CURRENCY";

    protected override void AwakeSingleton()
    {
        Initialize();
    }

    private void Start()
    {
        GenerateRewards();
    }

    private void Initialize()
    {
        LoadCurrency();
        zoneController.Initialize();
        rewardController.Initialize(reviveCost);
    }

    private void LoadCurrency()
    {
        if(PlayerPrefs.HasKey(currencySaveID))
        {
            currentCurrency = PlayerPrefs.GetInt(currencySaveID);
            //Display on UI via UIManager
        }
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

    public void Restart()
    {
        zoneController.RestartZone();
        rewardController.ClearCollectedRewards();
        GenerateRewards();
        rewardController.HideBombPanel();
    }

    public void Revive()
    {
        //Enough currency
        if(currentCurrency >= reviveCost)
        {
            currentCurrency -= reviveCost;
            rewardController.HideBombPanel();
            TriggerOnZonePrepared();
            return;
        }

        //Not enough currency
        UIManager.Instance.DisplayCurrencyWarning();
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
