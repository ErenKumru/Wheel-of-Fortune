using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinButton : ValidatedButton
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddButtonClickAction(SpinWheel);
        CardGameManager.OnSpinWheel += DeactivateButton;
        CardGameManager.OnZonePrepared += ActivateButton;
    }

    private void SpinWheel()
    {
        CardGameManager.Instance.wheelController.SelectReward();
    }
}
