using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeExplorationUI : MonoBehaviour
{
    [SerializeField] RectTransform uiElement;
    [SerializeField] Button leftArrowButton;
    [SerializeField] Button rightArrowButton;
    [SerializeField] Button numberButton;
    [SerializeField] TMP_Text numberText;

    int number = 1;
    
    private void Start()
    {
        leftArrowButton.onClick.AddListener(OnLeftArrowClick);
        rightArrowButton.onClick.AddListener(OnRightArrowClick);
        numberButton.onClick.AddListener(OnNumberClick);
        UpdateNumberText();
    }

    void OnLeftArrowClick()
    {
        if (number > 1)
        {
            number--;
            UpdateNumberText();
        }
    }

    void OnRightArrowClick()
    {
        number++;
        UpdateNumberText();
    }

    void OnNumberClick()
    {
        StartExploration();
    }

    void UpdateNumberText()
    {
        numberText.text = number.ToString();
    }

    void StartExploration()
    {
        Debug.Log("≈Ω«Ë Ω√¿€: " + number);        
    }

    public void SetUIPosition(Vector3 nodePosition)
    {        
        uiElement.position = nodePosition - new Vector3(0, 0.75f, 0);
        number = 1;
        UpdateNumberText();
    }

    public void HideUI()
    {
        uiElement.gameObject.SetActive(false);
    }
}
