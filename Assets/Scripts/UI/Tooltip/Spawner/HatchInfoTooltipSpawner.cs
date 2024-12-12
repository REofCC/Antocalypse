using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInfoTooltipSpawner : TooltipSpawner
{
    [SerializeField] AntType antType;
    public override bool CanCreateTooltip()
    {
        return true;
    }

    public override void UpdateTooltip(GameObject tooltip)
    {
        tooltip.GetComponent<HatchInfoTooltip>().SetAntTypeText(antType);
        tooltip.GetComponent<HatchInfoTooltip>().SetDescriptionText(antType);
    }
}
