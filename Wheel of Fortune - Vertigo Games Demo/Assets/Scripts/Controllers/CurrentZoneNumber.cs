using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentZoneNumber : ZoneNumber
{
    [SerializeField] private Image backgroundImage;

    public override void Initialize(ZoneController controller, int index)
    {
        transform.SetSiblingIndex(index);
        base.Initialize(controller, index);
    }

    protected override void SetNumberColor()
    {
        ZoneController.ZoneColors zoneColors = zoneController.GetZoneColors();
        int superZoneFrequency = zoneController.GetSuperZoneFrequency();
        int safeZoneFrequency = zoneController.GetSafeZoneFrequency();

        if (number % superZoneFrequency == 0)
            numberText.color = zoneColors.superZoneNumberColor;
        else if (number % safeZoneFrequency == 0)
            numberText.color = zoneColors.safeZoneNumberColor;
        else
            numberText.color = zoneColors.currentZoneNumberColor;
    }
}
