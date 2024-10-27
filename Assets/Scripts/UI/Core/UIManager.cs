using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instace;

    [SerializeField] ObjectivePanel objectivePanel;
    [SerializeField] ResourcePanel resourcePanel;
    [SerializeField] YearPanel yearPanel;
    [SerializeField] SpeedControlPanel speedControlPanel;
    [SerializeField] OptionsPanel optionsPanel;
    [SerializeField] ExplorationPanel explorationPanel;
    [SerializeField] EvolutionPanel evolutionPanel;
    [SerializeField] WinterProgressBar winterProgressBar;

    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
