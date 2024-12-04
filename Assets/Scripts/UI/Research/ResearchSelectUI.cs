using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchSelectUI : MonoBehaviour
{    
    [SerializeField] RectTransform specializationPanel;
    [SerializeField] RectTransform researchPanel;

    private void Start()
    {
        specializationPanel.gameObject.SetActive(true);
        researchPanel.gameObject.SetActive(false);
    }
    
    public void SpecializationCompleted()
    {
        specializationPanel.gameObject.SetActive(false);
        researchPanel.gameObject.SetActive(true);
    }
}
