using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegionResearchPanelUI : MonoBehaviour
{
    [SerializeField] RegionResearchInformationUI regionResearchInformationUI;
    [SerializeField] Button headButton;
    [SerializeField] Button mesosomaButton;
    [SerializeField] Button metasomaButton;
    AntType antType;

    [SerializeField] List<Button> leftButtons = new List<Button>();
    [SerializeField] List<Button> rightButtons = new List<Button>();

    private void Start()
    {
        headButton.onClick.AddListener(() => OnHeadButtonClicked());
        mesosomaButton.onClick.AddListener(() => OnMesosomaButtonClicked());
        metasomaButton.onClick.AddListener(() => OnMetasomaButtonClicked());

        for (int i = 0; i < leftButtons.Count; i++)
        {
            int index = i;
            leftButtons[i].onClick.AddListener(() => OnClickBuffButton(antType, GetBuffPartByIndex(index), 1));
        }

        for (int i = 0; i < rightButtons.Count; i++)
        {
            int index = i;
            rightButtons[i].onClick.AddListener(() => OnClickBuffButton(antType, GetBuffPartByIndex(index), 2));
        }
    }

    public void SetAntType(AntType _antType)
    {
        antType = _antType;
    }    

    //[LSH:TODO] Connect to the research manager
    void OnHeadButtonClicked()
    {
        leftButtons[0].GetComponentInChildren<TMP_Text>().text =  Managers.EvoManager.GetBuffDescription(antType, BuffPart.Head, 1);
        rightButtons[0].GetComponentInChildren<TMP_Text>().text = Managers.EvoManager.GetBuffDescription(antType, BuffPart.Head, 2);
    }

    void OnMesosomaButtonClicked()
    {
        leftButtons[1].GetComponentInChildren<TMP_Text>().text = Managers.EvoManager.GetBuffDescription(antType, BuffPart.Chest, 1);
        rightButtons[1].GetComponentInChildren<TMP_Text>().text = Managers.EvoManager.GetBuffDescription(antType, BuffPart.Chest, 2);
    }

    void OnMetasomaButtonClicked()
    {
        leftButtons[2].GetComponentInChildren<TMP_Text>().text = Managers.EvoManager.GetBuffDescription(antType, BuffPart.Belly, 1);
        rightButtons[2].GetComponentInChildren<TMP_Text>().text = Managers.EvoManager.GetBuffDescription(antType, BuffPart.Belly, 2);
    }

    void OnClickBuffButton(AntType _antType, BuffPart _buffPart, int branch)
    {
        Managers.EvoManager.AddCurrentBuff(_buffPart, branch);
        string description = Managers.EvoManager.GetBuffDescription(_antType, _buffPart, branch);
        regionResearchInformationUI.AddBuffDescription(description);
        
        if (_buffPart == BuffPart.Head)
        {
            leftButtons[0].interactable = false;
            rightButtons[0].interactable = false;
        }
        else if (_buffPart == BuffPart.Chest)
        {
            leftButtons[1].interactable = false;
            rightButtons[1].interactable = false;
        }
        else if (_buffPart == BuffPart.Belly)
        {
            leftButtons[2].interactable = false;
            rightButtons[2].interactable = false;
        }
    }

    BuffPart GetBuffPartByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return BuffPart.Head;
            case 1:
                return BuffPart.Chest;
            case 2:
                return BuffPart.Belly;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(index), "Invalid index for BuffPart");
        }
    }
}



