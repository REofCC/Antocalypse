using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YearPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI yearText;    

    private void Start()
    {
        
    }

    public void UpdateYear(int currentYear)
    {
        yearText.text = $"{currentYear}";
    }    
}