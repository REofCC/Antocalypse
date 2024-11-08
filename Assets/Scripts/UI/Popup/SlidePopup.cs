using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using static UIEnums;

public class SlidePopup  : MonoBehaviour
{
    [SerializeField] SlideDirection slideDirection;
    [SerializeField] PanelPopupManager popupManager;
    [SerializeField] RectTransform popupPanel;    
    [SerializeField] float slideDuration = 0.5f;
    bool isPopupOpen = false;
    [SerializeField] List<Button> buttons = new List<Button>();

    Vector2 hiddenPosition;
    Vector2 shownPosition;

    private void Start()
    {
        SetupDirections();

        popupPanel.anchoredPosition = hiddenPosition;

        SetButtonsInteractable(false);
    }

    void SetupDirections()
    {
        shownPosition = new Vector2(popupPanel.anchoredPosition.x, popupPanel.anchoredPosition.y);

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

        SetButtonsInteractable(false);

        popupPanel.DOAnchorPos(shownPosition, slideDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            SetButtonsInteractable(true);
        }).SetUpdate(true);

        isPopupOpen = true;
    }

    public void ClosePopup()
    {      
        if(!isPopupOpen)
        {
            return;
        }
        
        SetButtonsInteractable(false);

        popupPanel.DOAnchorPos(hiddenPosition, slideDuration).SetEase(Ease.OutQuad).SetUpdate(true);
        isPopupOpen = false;

        popupManager.PopupClosed(this);
    }

    public bool IsPopupOpen()
    {
        return isPopupOpen;
    }

    void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = interactable;
        }
    }
}
