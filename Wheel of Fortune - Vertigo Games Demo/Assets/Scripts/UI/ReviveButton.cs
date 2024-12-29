using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveButton : ValidatedButton
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddButtonClickAction(Revive);
    }

    private void Revive()
    {
        CardGameManager.Instance.Revive();
    }
}
