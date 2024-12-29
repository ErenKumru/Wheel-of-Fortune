using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ValidatedButton
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddButtonClickAction(Play);
    }

    private void Play()
    {
        UIManager.Instance.OpenGameCanvas();
    }
}
