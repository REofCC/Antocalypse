using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YearPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI yearText;
    [SerializeField] GameObject buffDebuffTooltip;

    private void Start()
    {
        buffDebuffTooltip.SetActive(false);
    }

    public void UpdateYear(int currentYear)
    {
        yearText.text = $"{currentYear}";
    }

    public void ShowBuffDebuffTooltip()
    {
        buffDebuffTooltip.SetActive(true);
    }

    public void HideBuffDebuffTooltip()
    {
        buffDebuffTooltip.SetActive(false);
    }
}