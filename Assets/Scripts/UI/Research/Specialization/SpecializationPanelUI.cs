using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecializationPanelUI : MonoBehaviour
{
    [SerializeField] ResearchSelectUI researchSelectUI;
    [SerializeField] RegionResearchPanelUI regionResearchPanelUI;
    [SerializeField] List<Button> typeButtons = new List<Button>();
    [SerializeField] List<Specialization> specializations = new List<Specialization>();
    [SerializeField] TMP_Text antTypeText;    
    [SerializeField] TMP_Text description;
    [SerializeField] Button progressButton;
    [SerializeField] Button exitButton;
    AntType antType;

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
        antTypeText.text = "";        
        description.text = "";
        progressButton.gameObject.SetActive(false);
    }

    void OnClickTypeButton(int typeNum)
    {
        antType = (AntType)typeNum;
        antTypeText.text = ((AntType)typeNum).ToString();        
        description.text = Managers.EvoManager.GetBasicDescription((AntType)typeNum);
        progressButton.gameObject.SetActive(true);
    }

    void OnClickProgressButton()
    {
        Managers.EvoManager.SetBuffTargetType(antType);
        GetComponentInParent<SlidePopup>().ClosePopup();
        regionResearchPanelUI.SetAntType(antType);
        StartCoroutine(WaitForPopupClose());
    }

    IEnumerator WaitForPopupClose()
    {
        yield return new WaitUntil(() => !GetComponentInParent<SlidePopup>().IsPopupOpen());
        researchSelectUI.SpecializationCompleted();
    }

    void ExitButtonClick()
    {        
        GetComponentInParent<SlidePopup>().ClosePopup();
        Redraw();
    }
}
