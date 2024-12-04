using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Recorder.AOV;
using UnityEngine;
using UnityEngine.UI;

public class SpecializationPanelUI : MonoBehaviour
{
    [SerializeField] ResearchSelectUI researchSelectUI;
    [SerializeField] List<Button> typeButtons = new List<Button>();
    [SerializeField] List<Specialization> specializations = new List<Specialization>();
    [SerializeField] TMP_Text antType;
    [SerializeField] TMP_Text specializationName;
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
        antType.text = "";
        specializationName.text = "";
        description.text = "";
        progressButton.gameObject.SetActive(false);
    }

    void OnClickTypeButton(int typeNum)
    {
        antType.text = specializations[typeNum].AntType;
        specializationName.text = specializations[typeNum].SpecializationName;
        description.text = specializations[typeNum].Description;
        progressButton.gameObject.SetActive(true);
    }

    void OnClickProgressButton()
    {
        //[TODO:LSH]SpecializationManager.Instance.AdoptSpecialization(specializations[typeNum]);
        GetComponentInParent<SlidePopup>().ClosePopup();
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
