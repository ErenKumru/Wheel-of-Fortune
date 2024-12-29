using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [Header("Zone Panel References")]
    [SerializeField] private RectTransform zonePanelRectTransform;
    [SerializeField] private ZoneNumber zoneNumberPrefab;
    [SerializeField] private int zoneNumberCount = 15;
    private float zoneStartPosX, zoneCurrentPosX, zoneEndPosX;
    private List<ZoneNumber> zoneNumbers;

    [Header("Current Zone References")]
    [SerializeField] private RectTransform currentZoneFrameRectTransform;
    [SerializeField] private CurrentZoneNumber currentZoneNumberPrefab;
    [SerializeField] private int currentZoneNumberCount = 3;
    private float currentZoneStartPosX, currentZoneCurrentPosX, currentZoneEndPosX;
    private List<ZoneNumber> currentZoneNumbers;
    private int currentZone = 1;

    [Header("Zone Values")]
    [SerializeField] private float moveTime = 1f;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private int safeZoneFrequency = 5;
    [SerializeField] private int superZoneFrequency = 30;
    [SerializeField] private ZoneColors zoneColors;

    public void Initialize()
    {
        InitializeZone();
    }

    private void InitializeZone()
    {
        float zoneNumberWidth = zoneNumberPrefab.rectTransform.rect.width;

        ResizeZonePanel(zoneNumberWidth);
        InitializeZonePanelValues(zoneNumberWidth);
        InitializeCurrentZoneValues();
        InitializeZoneNumbers(ref zoneNumbers, zoneNumberCount, zoneNumberPrefab, zonePanelRectTransform);
        InitializeZoneNumbers(ref currentZoneNumbers, currentZoneNumberCount, currentZoneNumberPrefab, currentZoneFrameRectTransform);
    }

    private void ResizeZonePanel(float zoneNumberWidth)
    {
        float zonePanelWidth = (zoneNumberCount - 2) * zoneNumberWidth;
        Vector2 sizeDelta = zonePanelRectTransform.sizeDelta;
        zonePanelRectTransform.sizeDelta = sizeDelta.SetXValue(zonePanelWidth);
    }

    private void InitializeZonePanelValues(float zoneNumberWidth)
    {
        zoneStartPosX = GetZoneMaxBoundary(zoneNumberWidth, zoneNumberCount);
        zoneEndPosX = -zoneStartPosX;
        zoneCurrentPosX = zonePanelRectTransform.rect.center.x;
    }

    private void InitializeCurrentZoneValues()
    {
        float currentZoneNumberWidth = currentZoneNumberPrefab.rectTransform.rect.width;
        currentZoneStartPosX = GetZoneMaxBoundary(currentZoneNumberWidth, currentZoneNumberCount);
        currentZoneEndPosX = -currentZoneStartPosX;
        currentZoneCurrentPosX = currentZoneFrameRectTransform.rect.center.x;
    }

    private float GetZoneMaxBoundary(float numberWidth, int numberCount)
    {
        return numberWidth * (numberCount - 1) / 2;
    }

    private void InitializeZoneNumbers(ref List<ZoneNumber> numberList, int numberCount, ZoneNumber numberPrefab, RectTransform parent)
    {
        numberList = new List<ZoneNumber>(numberCount);

        for(int i = 0; i < numberCount; i++)
        {
            ZoneNumber zoneNumber = Instantiate(numberPrefab, parent);
            zoneNumber.Initialize(this, i);
            numberList.Add(zoneNumber);
        }
    }

    public void ProgressZone()
    {
        currentZone++;

        foreach(ZoneNumber zoneNumber in zoneNumbers)
        {
            zoneNumber.Progress(zoneNumberCount, zoneStartPosX, zoneEndPosX, moveTime, delayTime);
        }

        foreach(ZoneNumber currentZoneNumber in currentZoneNumbers)
        {
            currentZoneNumber.Progress(currentZoneNumberCount, currentZoneStartPosX, currentZoneEndPosX, moveTime, delayTime);
        }
    }

    public void RestartZone()
    {
        currentZone = 1;
        ResetZoneNumbers(zoneNumbers);
        ResetZoneNumbers(currentZoneNumbers);
    }

    private void ResetZoneNumbers(List<ZoneNumber> numberList)
    {
        for(int i = 0; i < numberList.Count; i++)
        {
            ZoneNumber zoneNumber = numberList[i];
            zoneNumber.Initialize(this, i);
        }
    }

    public ZoneColors GetZoneColors()
    {
        return zoneColors;
    }

    public int GetCurrentZone()
    {
        return currentZone;
    }

    public int GetSafeZoneFrequency()
    {
        return safeZoneFrequency;
    }

    public int GetSuperZoneFrequency()
    {
        return superZoneFrequency;
    }

    [Serializable]
    public struct ZoneColors
    {
        public Color zoneNumberColor;
        public Color currentZoneNumberColor;
        public Color safeZoneNumberColor;
        public Color superZoneNumberColor;
    }
}
