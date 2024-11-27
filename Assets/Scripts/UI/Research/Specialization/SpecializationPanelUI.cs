using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecializationPanelUI : MonoBehaviour
{
    [SerializeField] ResearchSelectUI researchSelectUI;
    [SerializeField] List<Button> typeButtons = new List<Button>();
    [SerializeField] List<Specialization> specializations = new List<Specialization>();
    [SerializeField] TMP_Text description;
    [SerializeField] Button progressButton;
    [SerializeField] Button exitButton;

    private void Start()
    {        
        for (int i = 0; i < typeButtons.Count; i++)
        {
            int typeNum = i;
            typeButtons[i].onClick.AddListener(() => OnClickTypeButton(typeNum));
        }
        progressButton.onClick.AddListener(OnClickProgressButton);
        exitButton.onClick.AddListener(ExitButtonClick);
        Redraw();
    }

    void Redraw()
    {
        description.text = "";
        progressButton.gameObject.SetActive(false);
    }

    void OnClickTypeButton(int typeNum)
    {
        description.text = specializations[typeNum].GetDescription();
        progressButton.gameObject.SetActive(true);
    }

    void OnClickProgressButton()
    {        
        researchSelectUI.SpecializationCompleted();
        //[TODO:LSH]SpecializationManager.Instance.AdoptSpecialization(specializations[typeNum]);
        GetComponentInParent<SlidePopup>().ClosePopup();
    }

    void ExitButtonClick()
    {        
        GetComponentInParent<SlidePopup>().ClosePopup();
        Redraw();
    }
}
