using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionResearchPanelUI : MonoBehaviour
{
    [SerializeField] Button headButton;
    [SerializeField] Button mesosomaButton;
    [SerializeField] Button metasomaButton;

    private void Start()
    {
        headButton.onClick.AddListener(() => OnHeadButtonClicked());
        mesosomaButton.onClick.AddListener(() => OnMesosomaButtonClicked());
        metasomaButton.onClick.AddListener(() => OnMetasomaButtonClicked());
    }
    
    //[LSH:TODO] Connect to the research manager
    void OnHeadButtonClicked()
    {
        Debug.Log("Head button clicked");        
    }

    void OnMesosomaButtonClicked()
    {
        Debug.Log("Mesosoma button clicked");
    }

    void OnMetasomaButtonClicked()
    {
        Debug.Log("Metasoma button clicked");
    }
}
