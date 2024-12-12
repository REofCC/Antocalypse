using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HatchInfoTooltip : MonoBehaviour
{
    [SerializeField] TMP_Text antTypeText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] [TextArea] List<string> description = new List<string>();
    //[LSH:TODO] Type 열거형으로 받아야 함
    public void SetAntTypeText(TempAntType antType)
    {
        antTypeText.text = $"{antType.ToString()} Ant";
    }

    public void SetDescriptionText(TempAntType antType)
    {
        if(antType == TempAntType.Worker)
        {
            descriptionText.text = description[0];
        }
        else if (antType == TempAntType.Scout)
        {
            descriptionText.text = description[1];
        }
        else if (antType == TempAntType.Soldier)
        {
            descriptionText.text = description[2];
        }
        else if (antType == TempAntType.Queen)
        {
            descriptionText.text = description[3];
        }
    }

}
