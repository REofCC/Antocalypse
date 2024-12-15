using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionResearchInformationUI : MonoBehaviour
{
    [SerializeField] RectTransform descriptionContent;
    [SerializeField] GameObject descriptionPrefab;

    public void AddBuffDescription(string description)
    {
        GameObject descriptionPanel = Instantiate(descriptionPrefab, descriptionContent);
        descriptionPanel.GetComponentInChildren<TMPro.TMP_Text>().text = description;
    }
}
