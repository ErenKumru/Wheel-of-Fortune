using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentZoneNumber : ZoneNumber
{
    [SerializeField] private Image backgroundImage;

    public override void Initialize(int index)
    {
        transform.SetSiblingIndex(index);
        base.Initialize(index);
    }
}
