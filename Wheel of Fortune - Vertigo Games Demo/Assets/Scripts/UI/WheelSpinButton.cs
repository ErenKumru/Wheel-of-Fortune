using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinButton : ValidatedButton
{
    private void Awake()
    {
        AddButtonClickAction(SpinWheel);
    }

    private void SpinWheel()
    {
        CardGameManager.Instance.wheelController.RotateWheel();
    }
}
