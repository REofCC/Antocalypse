using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UIEnums;

public class PopupButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] MouseAction mouseAction;
    [SerializeField] SlidePopup slidePopup;
        
    public void OnPointerClick(PointerEventData eventData)
    {
        if (mouseAction == MouseAction.CLICK)
        {
            slidePopup.TogglePopup();
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseAction == MouseAction.HOVER)
        {
            slidePopup.OpenPopup();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseAction == MouseAction.HOVER)
        {
            slidePopup.ClosePopup();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (mouseAction == MouseAction.PRESS)
        {
            slidePopup.TogglePopup();
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (mouseAction == MouseAction.RELEASE)
        {
            slidePopup.TogglePopup();
        }
    }
}
