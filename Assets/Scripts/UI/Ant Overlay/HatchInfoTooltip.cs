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
    public void SetAntTypeText(AntType antType)
    {
        antTypeText.text = $"{antType.ToString()} Ant";
    }

    public void SetDescriptionText(AntType antType)
    {
        if(antType == AntType.Worker)
        {
            descriptionText.text = description[0];
        }
        else if (antType == AntType.Scout)
        {
            descriptionText.text = description[1];
        }
        else if (antType == AntType.Soldier)
        {
            descriptionText.text = description[2];
        }
        else if (antType == AntType.Queen)
        {
            descriptionText.text = description[3];
        }
    }

}
