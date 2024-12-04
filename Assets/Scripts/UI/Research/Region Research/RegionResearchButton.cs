using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegionResearchButton : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    //ResearchManager researchManager;
    //Research research;
    private void Start()
    {
        leftButton.onClick.AddListener(() => OnLeftButtonClicked());
        rightButton.onClick.AddListener(() => OnRightButtonClicked());
        leftButton.GetComponentInChildren<TMP_Text>().text = "research[0].GetDescription()";
        rightButton.GetComponentInChildren<TMP_Text>().text = "research[1].GetDescription()";
    }

    void OnLeftButtonClicked()
    {
        Debug.Log("Left button clicked");
    }

    void OnRightButtonClicked()
    {
        Debug.Log("Right button clicked");
    }
}
