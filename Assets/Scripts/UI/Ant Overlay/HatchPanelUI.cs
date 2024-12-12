using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatchPanelUI : MonoBehaviour
{
    [SerializeField] Button workerHatchButton;
    [SerializeField] Button scoutHatchButton;
    [SerializeField] Button soldierHatchButton;

    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        workerHatchButton.onClick.AddListener(() => OnAntHatchButtonClicked(AntType.Worker));
        scoutHatchButton.onClick.AddListener(() => OnAntHatchButtonClicked(AntType.Scout));
        soldierHatchButton.onClick.AddListener(() => OnAntHatchButtonClicked(AntType.Soldier));
    }

    void OnAntHatchButtonClicked(AntType antType)
    {        
        ActiveManager.Active.SpwanEgg(antType);
    }
}
