using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : ValidatedButton
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddButtonClickAction(ResetCurrency);
    }

    private void ResetCurrency()
    {
        CardGameManager.Instance.ResetCurrency();
    }
}
