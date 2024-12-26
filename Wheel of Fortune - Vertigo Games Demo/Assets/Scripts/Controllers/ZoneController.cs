using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    private void Awake()
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
        zonePanelRectTransform.sizeDelta = Vector2Utility.SetXValue(zonePanelRectTransform.sizeDelta, zonePanelWidth);
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

    //private void InitializeZoneNumbers()
    //{
    //    zoneNumbers = new List<ZoneNumber>(zoneNumberCount);

    //    for(int i = 0; i < zoneNumberCount; i++)
    //    {
    //        ZoneNumber zoneNumber = Instantiate(zoneNumberPrefab, zonePanelRectTransform);
    //        zoneNumber.Initialize(i);
    //        zoneNumbers.Add(zoneNumber);
    //    }
    //}

    //private void InitializeCurrentZoneNumbers()
    //{
    //    currentZoneNumbers = new List<ZoneNumber>(currentZoneNumberCount);

    //    for(int i = 0; i < currentZoneNumberCount; i++)
    //    {
    //        CurrentZoneNumber currentZoneNumber = Instantiate(currentZoneNumberPrefab, currentZoneFrameRectTransform);
    //        currentZoneNumber.Initialize(i);
    //        currentZoneNumbers.Add(currentZoneNumber);
    //    }
    //}

    private void InitializeZoneNumbers(ref List<ZoneNumber> numberList, int numberCount, ZoneNumber numberPrefab, RectTransform parent)
    {
        numberList = new List<ZoneNumber>(numberCount);

        for(int i = 0; i < numberCount; i++)
        {
            ZoneNumber zoneNumber = Instantiate(numberPrefab, parent);
            zoneNumber.Initialize(i);
            numberList.Add(zoneNumber);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ProgressZone();
        }
    }

    private void ProgressZone()
    {
        currentZone++;

        foreach(ZoneNumber zoneNumber in zoneNumbers)
        {
            zoneNumber.Progress(zoneNumberCount, zoneStartPosX, zoneEndPosX, moveTime);
        }

        foreach(ZoneNumber currentZoneNumber in currentZoneNumbers)
        {
            currentZoneNumber.Progress(currentZoneNumberCount, currentZoneStartPosX, currentZoneEndPosX, moveTime);
        }
    }
}
