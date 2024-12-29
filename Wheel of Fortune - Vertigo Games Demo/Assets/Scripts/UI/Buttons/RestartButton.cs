using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : ValidatedButton
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddButtonClickAction(Restart);
    }

    private void Restart()
    {
        CardGameManager.Instance.Restart();
    }
}
