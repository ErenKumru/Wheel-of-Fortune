using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ValidatedButton
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddButtonClickAction(ExitGame);
        CardGameManager.OnSpinWheel += DeactivateButton;
        CardGameManager.OnZonePrepared += ActivateButton;
    }

    protected override void ActivateButton()
    {
        if(CardGameManager.Instance.CanExit())
            base.ActivateButton();
    }

    private void ExitGame()
    {
        CardGameManager.Instance.ExitGame();
        UIManager.Instance.OpenMenuCanvas();
    }
}
