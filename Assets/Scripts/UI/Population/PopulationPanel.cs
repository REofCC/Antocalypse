using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulationPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI workerPopulationText;
    [SerializeField] TextMeshProUGUI scoutPopulationText;
    [SerializeField] TextMeshProUGUI soldierPopulationFoodText;

    private void Start()
    {
        Managers.Population.OnPopulationChange += UpdatePopulation;
        UpdatePopulation(AntType.Worker);
        UpdatePopulation(AntType.Scout);
        UpdatePopulation(AntType.Soldier);
    }

    private void OnDestroy()
    {
        Managers.Population.OnPopulationChange -= UpdatePopulation;
    }

    void UpdatePopulation(AntType type)
    {
        switch (type)
        {
            case AntType.Worker:
                workerPopulationText.text = $"{Managers.Population.GetCurrnetPopulation(AntType.Worker)}/ {Managers.Population.GetMaxPopulation(AntType.Worker)}";
                break;
            case AntType.Scout:
                scoutPopulationText.text = $"{Managers.Population.GetCurrnetPopulation(AntType.Scout)}/ {Managers.Population.GetMaxPopulation(AntType.Scout)}";
                break;
            case AntType.Soldier:
                soldierPopulationFoodText.text = $"{Managers.Population.GetCurrnetPopulation(AntType.Soldier)}/ {Managers.Population.GetMaxPopulation(AntType.Soldier)}";
                break;
            default:
                Debug.LogError("Invalid Ant type");
                break;
        }
    }
}
