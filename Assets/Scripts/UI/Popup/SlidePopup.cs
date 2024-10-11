using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using static UIEnums;

public class SlidePopup  : MonoBehaviour
{
    [SerializeField] SlideDirection slideDirection;
    [SerializeField] PanelPopupManager popupManager;
    [SerializeField] RectTransform popupPanel;    
    [SerializeField] float slideDuration = 0.5f;
    bool isPopupOpen = false;

    Vector2 hiddenPosition;
    Vector2 shownPosition;

    private void Start()
    {
        SetupDirections();

        popupPanel.anchoredPosition = hiddenPosition;
    }

    void SetupDirections()
    {
        shownPosition = Vector2.zero;

        switch (slideDirection)
        {
            case SlideDirection.UP:
                hiddenPosition = new Vector2(popupPanel.anchoredPosition.x, -popupPanel.rect.height);
                break;
            case SlideDirection.DOWN:
                hiddenPosition = new Vector2(popupPanel.anchoredPosition.x, popupPanel.rect.height);
                break;
            case SlideDirection.LEFT:
                hiddenPosition = new Vector2(popupPanel.rect.width, popupPanel.anchoredPosition.y);
                break;
            case SlideDirection.RIGHT:
                hiddenPosition = new Vector2(-popupPanel.rect.width, popupPanel.anchoredPosition.y);
                break;
        }
    }

    public void TogglePopup()
    {
        popupManager.TogglePopup(this);    
    }


    public void OpenPopup()
    {
        if(isPopupOpen)
        {
            return;
        }

        popupPanel.DOAnchorPos(shownPosition, slideDuration).SetEase(Ease.OutQuad);
        isPopupOpen = true;
    }

    public void ClosePopup()
    {      
        if(!isPopupOpen)
        {
            return;
        }

        popupPanel.DOAnchorPos(hiddenPosition, slideDuration).SetEase(Ease.OutQuad);
        isPopupOpen = false;

        popupManager.PopupClosed(this);
    }

    public bool IsPopupOpen()
    {
        return isPopupOpen;
    }
}
