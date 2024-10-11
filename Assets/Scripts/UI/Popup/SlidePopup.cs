using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlidePopup  : MonoBehaviour
{
    [SerializeField] RectTransform popupPanel;    
    [SerializeField] float slideDuration = 0.5f;
    bool isPopupOpen = false;

    Vector2 hiddenPosition;
    Vector2 shownPosition;

    private void Start()
    {
        hiddenPosition = popupPanel.anchoredPosition;
        shownPosition = hiddenPosition + new Vector2(0, popupPanel.rect.height);

        popupPanel.anchoredPosition = hiddenPosition;
    }

    public void TogglePopup()
    {
        if (isPopupOpen)
        {
            ClosePopup();
        }
        else
        {
            OpenPopup();
        }

        isPopupOpen = !isPopupOpen;
    }


    public void OpenPopup()
    {
        PopupManager.Instance.OpenPopup(this);

        popupPanel.DOAnchorPos(shownPosition, slideDuration).SetEase(Ease.OutQuad);
        isPopupOpen = true;
    }

    public void ClosePopup()
    {        
        popupPanel.DOAnchorPos(hiddenPosition, slideDuration).SetEase(Ease.OutQuad);
        isPopupOpen = false;

        PopupManager.Instance.ClosePopup(this);
    }
}
