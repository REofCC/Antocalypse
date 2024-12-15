using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    [SerializeField] Image eventImage;
    [SerializeField] TMP_Text eventDescription;
    [SerializeField] Button eventAcceptButton;
    [SerializeField] Button eventDeclineButton;
    EventData eventData;

    private void Start()
    {
        eventAcceptButton.onClick.AddListener(() => AcceptAction());
        eventDeclineButton.onClick.AddListener(() => DeclineAction());
    }

    public void SetEventPanel(Sprite image, string description)
    {
        eventImage.sprite = image;
        eventDescription.text = description;

    }

    void AcceptAction()
    {

    }

    void DeclineAction()
    {

    }
}
