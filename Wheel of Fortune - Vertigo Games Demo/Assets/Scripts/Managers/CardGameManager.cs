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

    public void GenerateRewards()
    {
        rewardController.GenerateNewRewards();
        wheelController.DisplayNewRewards(rewardController.GetPossibleRewards());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            GenerateRewards();
        }
    }
}
