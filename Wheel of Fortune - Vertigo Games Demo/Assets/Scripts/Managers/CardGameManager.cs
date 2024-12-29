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

    private bool canExit;

    protected override void AwakeSingleton()
    {
        Initialize();
    }

    private void Start()
    {
        LoadCurrency();
        GenerateRewards();
    }

    private void Initialize()
    {
        zoneController.Initialize();
        rewardController.Initialize(reviveCost);
    }

    private void LoadCurrency()
    {
        if(PlayerPrefs.HasKey(currencySaveID))
        {
            currentCurrency = PlayerPrefs.GetInt(currencySaveID);
            UIManager.Instance.UpdateCurrencyText(currentCurrency);
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
            UIManager.Instance.UpdateCurrencyText(currentCurrency);
            rewardController.HideBombPanel();
            TriggerOnZonePrepared();
            return;
        }

        //Not enough currency
        UIManager.Instance.DisplayCurrencyWarning();
    }

    public void ResetCurrency()
    {
        currentCurrency = 100;
        SaveCurrency();
        UIManager.Instance.UpdateCurrencyText(currentCurrency);
    }

    private void AddCurrency()
    {
        int amount = rewardController.GetCollectedCurrencyAmount();
        currentCurrency += amount;
        UIManager.Instance.UpdateCurrencyText(currentCurrency);
        SaveCurrency();
    }

    public void SaveCurrency()
    {
        PlayerPrefs.SetInt(currencySaveID, currentCurrency);
    }

    public bool CanExit()
    {
        int currentZone = zoneController.GetCurrentZone();
        int safeZoneFrequency = zoneController.GetSafeZoneFrequency();
        int superZoneFrequency = zoneController.GetSuperZoneFrequency();

        return canExit && (currentZone % safeZoneFrequency == 0 || currentZone % superZoneFrequency == 0);
    }

    public void ExitGame()
    {
        AddCurrency();
        Restart();
    }

    public void TriggerOnSpinWheel()
    {
        canExit = false;
        OnSpinWheel?.Invoke();
    }

    public void TriggerOnZonePrepared()
    {
        canExit = true;
        OnZonePrepared?.Invoke();
    }
}
