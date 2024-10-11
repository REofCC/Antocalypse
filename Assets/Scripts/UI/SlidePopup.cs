using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlidePopup  : MonoBehaviour
{
    [SerializeField] RectTransform popupPanel;
    [SerializeField] Button triggerButton;
    [SerializeField] float slideDuration = 0.5f;
    bool isPopupOpen = false;

    Vector2 hiddenPosition;
    Vector2 shownPosition;

    private void Start()
    {
        hiddenPosition = popupPanel.anchoredPosition;
        shownPosition = hiddenPosition + new Vector2(0, popupPanel.rect.height);

        triggerButton.onClick.AddListener(TogglePopup);
    }

    void TogglePopup()
    {
        if (isPopupOpen)
        {
            popupPanel.DOAnchorPos(hiddenPosition, slideDuration).SetEase(Ease.OutQuad);
        }
        else
        {
            popupPanel.DOAnchorPos(shownPosition, slideDuration).SetEase(Ease.OutQuad);
        }

        isPopupOpen = !isPopupOpen;
    }
}
